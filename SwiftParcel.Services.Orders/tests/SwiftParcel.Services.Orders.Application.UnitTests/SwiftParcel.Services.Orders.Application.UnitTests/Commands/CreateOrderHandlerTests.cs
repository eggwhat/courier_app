using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Commands
{
    public class CreateOrderHandlerTests
    {
        private readonly CreateOrderHandler _createOrderHandler;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<ICommandDispatcher> _commandDispatcherMock;

        public CreateOrderHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _commandDispatcherMock = new Mock<ICommandDispatcher>();
            _createOrderHandler = new CreateOrderHandler(_customerRepositoryMock.Object, _commandDispatcherMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithValidCustomerAndSwiftParcelCompany_ShouldSendCreateOrderSwiftParcelCommand()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var parcelId = Guid.NewGuid();
            var command = new CreateOrder(orderId, customerId, parcelId, "", "", new Address(), Company.SwiftParcel);
            var cancellationToken = new CancellationToken();

            _customerRepositoryMock.Setup(repo => repo.ExistsAsync(command.CustomerId)).ReturnsAsync(true);

            // Act
            await _createOrderHandler.HandleAsync(command, cancellationToken);

            // Assert
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<CreateOrderSwiftParcel>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithValidCustomerAndMiniCurrierCompany_ShouldSendCreateOrderMiniCurrierCommand()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var parcelId = Guid.NewGuid();
            var command = new CreateOrder(orderId, customerId, parcelId, "", "", new Address(), Company.MiniCurrier);
            var cancellationToken = new CancellationToken();

            _customerRepositoryMock.Setup(repo => repo.ExistsAsync(command.CustomerId)).ReturnsAsync(true);

            // Act
            await _createOrderHandler.HandleAsync(command, cancellationToken);

            // Assert
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<CreateOrderMiniCurrier>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidCustomer_ShouldThrowCustomerNotFoundException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var parcelId = Guid.NewGuid();
            var command = new CreateOrder(orderId, customerId, parcelId, "", "", new Address(), Company.MiniCurrier);
            var cancellationToken = new CancellationToken();

            _customerRepositoryMock.Setup(repo => repo.ExistsAsync(command.CustomerId)).ReturnsAsync(false);

            // Act & Assert

            Func<Task> act = async () => await _createOrderHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<CustomerNotFoundException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidCompany_ShouldThrowCompanyNotFoundException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var parcelId = Guid.NewGuid();
            var command = new CreateOrder(orderId, customerId, parcelId, "", "", new Address(), (Company)int.MaxValue);
            var cancellationToken = new CancellationToken();

            _customerRepositoryMock.Setup(repo => repo.ExistsAsync(command.CustomerId)).ReturnsAsync(true);

            // Act & Assert
            Func<Task> act = async () => await _createOrderHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<CompanyNotFoundException>();
        }
    }
}
