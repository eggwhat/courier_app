using SwiftParcel.Services.Orders.Application.Events.External;
using SwiftParcel.Services.Orders.Application.Events.External.Handlers;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Events;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Events.External.Handlers
{
    public class DeliveryPickedUpHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly Mock<IEventMapper> _eventMapperMock;
        private readonly DeliveryPickedUpHandler _deliveryPickedUpHandler;

        public DeliveryPickedUpHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _eventMapperMock = new Mock<IEventMapper>();
            _deliveryPickedUpHandler = new DeliveryPickedUpHandler(_orderRepositoryMock.Object, _messageBrokerMock.Object, _eventMapperMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithOrderNotFound_ShouldThrowOrderNotFoundException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var @event = new DeliveryPickedUp(Guid.NewGuid(), orderId, DateTime.UtcNow);
            var cancellationToken = new CancellationToken();

            _orderRepositoryMock.Setup(repository => repository.GetAsync(@event.OrderId))
                .ReturnsAsync((Order)null);

            // Act & Assert
            Func<Task> act = async () => await _deliveryPickedUpHandler.HandleAsync(@event, cancellationToken);
            await act.Should().ThrowAsync<OrderNotFoundException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidOrderStatus_ShouldThrowCannotChangeOrderStateException()
        {
            // Arrange
            var deliveryId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var deliveryDate = DateTime.UtcNow;
            var @event = new DeliveryPickedUp(deliveryId, orderId, deliveryDate);
            var cancellationToken = new CancellationToken();

            var order = Order.Create(new AggregateId(orderId), Guid.NewGuid(), OrderStatus.Approved,
                               DateTime.Now, "validName", "valid@Email.com",
                                              new Address("street", "buildingNumber", "apartmentNumber", "city", "00-433", "country"));

            _orderRepositoryMock.Setup(repo => repo.GetAsync(@event.OrderId)).ReturnsAsync(order);

            // Act && Assert
            Func<Task> act = async () => await _deliveryPickedUpHandler.HandleAsync(@event, cancellationToken);
            await act.Should().ThrowAsync<CannotChangeOrderStateException>();
        }

        [Fact]
        public async Task HandleAsync_WithValidOrder_ShouldSetOrderStatusToPickedUp()
        {
            // Arrange
            var deliveryId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var deliveryDate = DateTime.UtcNow;
            var @event = new DeliveryPickedUp(deliveryId, orderId, deliveryDate);
            var cancellationToken = new CancellationToken();

            var order = Order.Create(new AggregateId(orderId), Guid.NewGuid(), OrderStatus.Confirmed,
                               DateTime.Now, "validName", "valid@Email.com",
                                              new Address("street", "buildingNumber", "apartmentNumber", "city", "00-433", "country"));

            _orderRepositoryMock.Setup(repo => repo.GetAsync(@event.OrderId)).ReturnsAsync(order);

            // Act
            await _deliveryPickedUpHandler.HandleAsync(@event, cancellationToken);

            // Assert
            order.Status.Should().Be(OrderStatus.PickedUp);
            order.Events.Should().ContainEquivalentOf(new OrderStateChanged(order));
        }
    }
}
