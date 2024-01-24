using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;


namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class ApproveOrderOfficeWorkerHandler: ICommandHandler<ApproveOrderOfficeWorker>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly IAppContext _appContext;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ApproveOrderOfficeWorkerHandler(IOrderRepository orderRepository, IMessageBroker messageBroker,
            IEventMapper eventMapper, IAppContext appContext, ICommandDispatcher commandDispatcher,
            IDateTimeProvider dateTimeProvider)
        {
            _orderRepository = orderRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _appContext = appContext;
            _commandDispatcher = commandDispatcher;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task HandleAsync(ApproveOrderOfficeWorker command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(command.OrderId);
            if (order is null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            var identity = _appContext.Identity;
            if (!identity.IsOfficeWorker)
            {
                throw new UnauthorizedOrderAccessException(command.OrderId, identity.Id);
            }
            var decisionDate = _dateTimeProvider.Now;
            order.ApproveByOfficeWorker(decisionDate);
            if (order.CustomerId == Guid.Empty || order.CustomerId == null)
            {
                order.Confirm();
            }
            await _orderRepository.UpdateAsync(order);
            var events = _eventMapper.MapAll(order.Events);
            await _messageBroker.PublishAsync(events.ToArray());

            await _commandDispatcher.SendAsync(new SendApprovalEmail(order.Id, decisionDate,
            order.BuyerName, order.BuyerEmail, order.BuyerAddress, order.Parcel));
        }
    }
}