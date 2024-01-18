using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
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
    public class ConfirmOrderSwiftParcelHandlerTests
    {
        private readonly ConfirmOrderSwiftParcelHandler _confirmOrderSwiftParcelHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly Mock<IEventMapper> _eventMapperMock;
        private readonly Mock<IAppContext> _appContextMock;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

        public ConfirmOrderSwiftParcelHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _eventMapperMock = new Mock<IEventMapper>();
            _appContextMock = new Mock<IAppContext>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _confirmOrderSwiftParcelHandler = new ConfirmOrderSwiftParcelHandler(_orderRepositoryMock.Object, 
                _messageBrokerMock.Object, _eventMapperMock.Object, _appContextMock.Object, _dateTimeProviderMock.Object);
        }


        [Fact]
        public async Task HandleAsync_WithValidOrder_ShouldConfirmOrderAndUpdateRepository()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.SwiftParcel);
            var command = new ConfirmOrderSwiftParcel(confirmCommand);

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
            var identityContext = new IdentityContext(customerId.ToString(), "user",
                true, new Dictionary<string, string>());
            var cancellationToken = new CancellationToken();

            _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(DateTime.Now);

            // Act
            await _confirmOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.UpdateAsync(order), Times.Once);
            _eventMapperMock.Verify(mapper => mapper.MapAll(order.Events), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidState_ShouldThrowCannotChangeOrderStateException()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.SwiftParcel);
            var command = new ConfirmOrderSwiftParcel(confirmCommand);

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
            var order = Order.Create(command.OrderId, customerId, OrderStatus.PickedUp, DateTime.Now,
                               "name", "valid@email.com", validAddress);
            var identityContext = new IdentityContext(customerId.ToString(), "user",
                true, new Dictionary<string, string>());
            var cancellationToken = new CancellationToken();

            _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(DateTime.Now);

            // Act & Assert
            Func<Task> act = async () => await _confirmOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<CannotChangeOrderStateException>();
        }

        [Fact]
        public async Task HandleAsync_WithNullOrder_ShouldThrowOrderNotFoundException()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.SwiftParcel);
            var command = new ConfirmOrderSwiftParcel(confirmCommand);

            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync((Order)null);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            Func<Task> act = async () => await _confirmOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<OrderNotFoundException>();
        }

        [Fact]
        public async Task HandleAsync_WithUnauthorizedIdentity_ShouldThrowUnauthorizedOrderAccessException()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.SwiftParcel);
            var command = new ConfirmOrderSwiftParcel(confirmCommand);

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
            var order = Order.Create(command.OrderId, customerId, OrderStatus.WaitingForDecision, DateTime.Now,
                               "name", "valid@email.com", validAddress);
            var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "",
                true, new Dictionary<string, string>());
            _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            Func<Task> act = async () => await _confirmOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<UnauthorizedOrderAccessException>();
        }

        [Fact]
        public async Task HandleAsync_WithExpiredOrderRequest_ShouldThrowOrderRequestExpiredException()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.SwiftParcel);
            var command = new ConfirmOrderSwiftParcel(confirmCommand);

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
            var order = Order.Create(command.OrderId, customerId, OrderStatus.WaitingForDecision, DateTime.Now.AddMonths(-1),
                               "name", "valid@email.com", validAddress);
            var identityContext = new IdentityContext(customerId.ToString(), "user",
                true, new Dictionary<string, string>());
            _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(DateTime.Now);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            Func<Task> act = async () => await _confirmOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<OrderRequestExpiredException>();
        }
    }
}
