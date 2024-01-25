using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.Events;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Commands
{
    public class DeleteOrderHandlerTests
    {
        private readonly DeleteOrderHandler _deleteOrderHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IAppContext> _appContextMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;

        public DeleteOrderHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _appContextMock = new Mock<IAppContext>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _deleteOrderHandler = new DeleteOrderHandler(_orderRepositoryMock.Object, _appContextMock.Object, _messageBrokerMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithOrderNotFound_ShouldThrowOrderNotFoundException()
        {
            // Arrange
            var command = new DeleteOrder(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            _orderRepositoryMock.Setup(repository => repository.GetAsync(command.OrderId))
                .ReturnsAsync((Order)null);

            // Act & Assert
            Func<Task> act = async () => await _deleteOrderHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<OrderNotFoundException>();
        }

        [Fact]
        public async Task HandleAsync_WithAuthenticatedUserAndNotOfficeWorkerAndNotOwner_ShouldThrowUnauthorizedOrderAccessException()
        {
            // Arrange
            var command = new DeleteOrder(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            var validAddress = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "32",
                BuildingNumber = "32",
                ZipCode = "00-323",
                Street = "Street"
            };
            var customerId = Guid.NewGuid();
            var order = Order.Create(command.OrderId, customerId, OrderStatus.Approved, DateTime.Now,
                               "name", "valid@email.com", validAddress);

            var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "user", true, null);

            _orderRepositoryMock.Setup(repository => repository.GetAsync(command.OrderId))
                .ReturnsAsync(order);

            _appContextMock.Setup(context => context.Identity).Returns(identityContext);

            // Act & Assert
            Func<Task> act = async () => await _deleteOrderHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<UnauthorizedOrderAccessException>();
        }

        [Fact]
        public async Task HandleAsync_WithValidIdentityAndCanBeDeletedSetToFalse_ShouldThrowCannotDeleteOrderException()
        {
            // Arrange
            var command = new DeleteOrder(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            var validAddress = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "32",
                BuildingNumber = "32",
                ZipCode = "00-323",
                Street = "Street"
            };
            var customerId = Guid.NewGuid();
            var order = Order.Create(command.OrderId, customerId, OrderStatus.Approved, DateTime.Now,
                               "name", "valid@email.com", validAddress);

            var identityContext = new IdentityContext(customerId.ToString(), "user", true, null);

            _orderRepositoryMock.Setup(repository => repository.GetAsync(command.OrderId))
                .ReturnsAsync(order);

            _appContextMock.Setup(context => context.Identity).Returns(identityContext);

            // Act & Assert
            Func<Task> act = async () => await _deleteOrderHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<CannotDeleteOrderException>();
        }

        [Fact]
        public async Task HandleAsync_WithValidIdentity_ShouldInvokeDeleteAsyncOrder()
        {
            // Arrange
            var command = new DeleteOrder(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            var validAddress = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "32",
                BuildingNumber = "32",
                ZipCode = "00-323",
                Street = "Street"
            };
            var customerId = Guid.NewGuid();
            var order = Order.Create(command.OrderId, customerId, OrderStatus.Cancelled, DateTime.Now,
                               "name", "valid@email.com", validAddress);

            var identityContext = new IdentityContext(customerId.ToString(), "user", true, null);

            _orderRepositoryMock.Setup(repository => repository.GetAsync(command.OrderId))
                .ReturnsAsync(order);

            _appContextMock.Setup(context => context.Identity).Returns(identityContext);

            // Act
            await _deleteOrderHandler.HandleAsync(command, cancellationToken);

            // Assert
            _orderRepositoryMock.Verify(repository => repository.DeleteAsync(order.Id), Times.Once);
            _messageBrokerMock.Verify(broker => broker.PublishAsync(It.IsAny<OrderDeleted>()), Times.Once);
        }
    }
}
