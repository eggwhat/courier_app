using System.Threading.Tasks;
using Convey.CQRS.Events;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Application.Services;

namespace SwiftParcel.Services.Orders.Application.Events.External.Handlers
{
    public class ResourceReservationCanceledHandler : IEventHandler<ResourceReservationCancelled>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

        public ResourceReservationCanceledHandler(IOrderRepository orderRepository, IMessageBroker messageBroker,
            IEventMapper eventMapper)
        {
            _orderRepository = orderRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public async Task HandleAsync(ResourceReservationCancelled @event, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(@event.ResourceId, @event.ReservationDate);
            if (order is null)
            {
                throw new OrderForReservedCourierNotFoundException(@event.ResourceId, @event.ReservationDate);
            }

            order.Cancel($"Reservation cancelled at: {@event.ReservationDate}");
            await _orderRepository.UpdateAsync(order);
            var events = _eventMapper.MapAll(order.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}