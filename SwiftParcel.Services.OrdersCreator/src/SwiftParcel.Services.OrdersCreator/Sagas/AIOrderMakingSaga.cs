using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronicle;
using Convey.MessageBrokers;
using SwiftParcel.Services.OrdersCreator.Commands;
using SwiftParcel.Services.OrdersCreator.Commands.External;
using SwiftParcel.Services.OrdersCreator.Events;
using SwiftParcel.Services.OrdersCreator.Events.External;
using SwiftParcel.Services.OrdersCreator.Services;
using SwiftParcel.Services.OrdersCreator.Services.Clients;

namespace SwiftParcel.Services.OrdersCreator.Sagas
{
    public class AIOrderMakingSaga: Saga<AIMakingOrderData>,
        ISagaStartAction<MakeOrder>,
        ISagaAction<OrderCreated>,
        ISagaAction<ParcelAddedToOrder>,
        ISagaAction<CourierAssignedToOrder>,
        ISagaAction<OrderApproved>
    {
        private const string SagaHeader = "Saga";
        private readonly IResourceReservationsService _resourceReservationsService;
        private readonly ICouriersServiceClient _couriersServiceClient;
        private readonly IBusPublisher _publisher;
        private readonly ICorrelationContextAccessor _accessor;
        private readonly ILogger<AIOrderMakingSaga> _logger;

        public AIOrderMakingSaga(IResourceReservationsService resourceReservationsService,
            ICouriersServiceClient couriersServiceClient, IBusPublisher publisher,
            ICorrelationContextAccessor accessor, ILogger<AIOrderMakingSaga> logger)
        {
            _resourceReservationsService = resourceReservationsService;
            _couriersServiceClient = couriersServiceClient;
            _publisher = publisher;
            _accessor = accessor;
            _logger = logger;
            _accessor.CorrelationContext = new CorrelationContext
            {
                User = new CorrelationContext.UserContext()
            };
        }

        public override SagaId ResolveId(object message, ISagaContext context)
            => message switch
            {
                MakeOrder m => (SagaId) m.OrderId.ToString(),
                OrderCreated m => (SagaId) m.OrderId.ToString(),
                ParcelAddedToOrder m => (SagaId) m.OrderId.ToString(),
                CourierAssignedToOrder m => (SagaId) m.OrderId.ToString(),
                OrderApproved m => m.OrderId.ToString(),
                _ => base.ResolveId(message, context)
            };

        public async Task HandleAsync(MakeOrder message, ISagaContext context)
        {
            _logger.LogInformation($"Started a saga for order: '{message.OrderId}'.");
            Data.ParcelIds.Add(message.ParcelId);
            Data.OrderId = message.OrderId;
            Data.CustomerId = message.CustomerId;

            await _publisher.PublishAsync(new CreateOrder(Data.OrderId, message.CustomerId ?? Guid.Empty),
                messageContext: _accessor.CorrelationContext,
                headers: new Dictionary<string, object>
                {
                    [SagaHeader] = SagaStates.Pending.ToString()
                });
        }

        public async Task HandleAsync(OrderCreated message, ISagaContext context)
        {
            var tasks = Data.ParcelIds.Select(id =>
                _publisher.PublishAsync(new AddParcelToOrder(Data.OrderId, id, Data.CustomerId),
                    messageContext: _accessor.CorrelationContext,
                    headers: new Dictionary<string, object>
                    {
                        [SagaHeader] = SagaStates.Pending.ToString()
                    }));

            await Task.WhenAll(tasks);
        }

        public async Task HandleAsync(ParcelAddedToOrder message, ISagaContext context)
        {
            Data.AddedParcelIds.Add(message.ParcelId);
            if (!Data.AllPackagesAddedToOrder)
            {
                return;
            }

            _logger.LogInformation("Searching for a courier...");
            var courier = await _couriersServiceClient.GetBestAsync();
            Data.CourierId = courier.Id;
            _logger.LogInformation($"Found a courier with id: '{courier.Id}' for {courier.PricePerService}$.");

            _logger.LogInformation($"Reserving a date for courier: '{courier.Id}'...");
            var reservation = await _resourceReservationsService.GetBestAsync(Data.CourierId);
            Data.ReservationDate = reservation.DateTime;
            Data.ReservationPriority = reservation.Priority;
            _logger.LogInformation($"Reserved a date: {Data.ReservationDate.Date} for courier: '{courier.Id}'.");

            await _publisher.PublishAsync(
                new AssignCourierToOrder(Data.OrderId, Data.CourierId, Data.ReservationDate),
                messageContext: _accessor.CorrelationContext,
                headers: new Dictionary<string, object>
                {
                    [SagaHeader] = SagaStates.Pending.ToString()
                });
        }

        public Task HandleAsync(CourierAssignedToOrder message, ISagaContext context)
            => _publisher.PublishAsync(new ReserveResource(Data.CourierId, Data.CustomerId,
                    Data.ReservationDate, Data.ReservationPriority),
                messageContext: _accessor.CorrelationContext,
                headers: new Dictionary<string, object>
                {
                    [SagaHeader] = SagaStates.Pending.ToString()
                });

        public async Task HandleAsync(OrderApproved message, ISagaContext context)
        {
            _logger.LogInformation($"Completed a saga for order: '{message.OrderId}'.");
            await _publisher.PublishAsync(new MakeOrderCompleted(message.OrderId),
                messageContext: _accessor.CorrelationContext,
                headers: new Dictionary<string, object>
                {
                    [SagaHeader] = SagaStates.Completed.ToString()
                });

            await CompleteAsync();
        }

        public Task CompensateAsync(MakeOrder message, ISagaContext context)
            => Task.CompletedTask;

        public Task CompensateAsync(OrderCreated message, ISagaContext context)
            => Task.CompletedTask;

        public Task CompensateAsync(ParcelAddedToOrder message, ISagaContext context)
            => _publisher.PublishAsync(new CancelOrder(message.OrderId, "Because I'm saga"),
                messageContext: _accessor.CorrelationContext,
                headers: new Dictionary<string, object>
                {
                    [SagaHeader] = SagaStates.Rejected.ToString()
                });

        public Task CompensateAsync(CourierAssignedToOrder message, ISagaContext context)
            => Task.CompletedTask;

        public Task CompensateAsync(OrderApproved message, ISagaContext context)
            => Task.CompletedTask;
    }
}