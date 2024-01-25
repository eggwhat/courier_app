using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using MongoDB.Driver;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.Events;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Infrastructure.Contexts;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Commands
{
    public class ApproveOrderOfficeWorkerHandlerTests
    {
        private readonly ApproveOrderOfficeWorkerHandler _approveOrdersOfficeWorkerHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly Mock<IEventMapper> _eventMapperMock;
        private readonly Mock<IAppContext> _appContextMock;
        private readonly Mock<ICommandDispatcher> _commandDispatcherMock;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

        public ApproveOrderOfficeWorkerHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _eventMapperMock = new Mock<IEventMapper>();
            _appContextMock = new Mock<IAppContext>();
            _commandDispatcherMock = new Mock<ICommandDispatcher>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _approveOrdersOfficeWorkerHandler = new ApproveOrderOfficeWorkerHandler(_orderRepositoryMock.Object, _messageBrokerMock.Object, _eventMapperMock.Object, _appContextMock.Object, _commandDispatcherMock.Object, _dateTimeProviderMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithValidOrderAndOfficeWorkerIdentity_ShouldApproveOrderAndSendEmail()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new ApproveOrderOfficeWorker(orderId);

            var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "officeworker",
                true, new Dictionary<string, string>());

            var order = Order.Create(new AggregateId(orderId), Guid.NewGuid(), OrderStatus.WaitingForDecision,
                DateTime.Now, "validName", "valid@email.com", 
                new Address("street", "buildingNumber", "apartmentNumber", "city", "00-433", "country"));
            
            _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);

            var cancellationToken = new CancellationToken();

            // Act
            await _approveOrdersOfficeWorkerHandler.HandleAsync(command, cancellationToken);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.UpdateAsync(order), Times.Once);
            _eventMapperMock.Verify(mapper => mapper.MapAll(order.Events), Times.Once);
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<SendApprovalEmail>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidOrder_ShouldThrowOrderNotFoundException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new ApproveOrderOfficeWorker(orderId);
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync((Order)null);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            Func<Task> act = async () => await _approveOrdersOfficeWorkerHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<OrderNotFoundException>();
        }

        [Fact]
        public async Task HandleAsync_WithNonOfficeWorkerIdentity_ShouldThrowUnauthorizedOrderAccessException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new ApproveOrderOfficeWorker(orderId);

            var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "customer",
                true, new Dictionary<string, string>());
            var cancellationToken = new CancellationToken();

            _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);
            var order = Order.Create(new AggregateId(orderId), Guid.NewGuid(), OrderStatus.WaitingForDecision,
                DateTime.Now, "validName", "valid@email.com", 
                new Address("street", "buildingNumber", "apartmentNumber", "city", "00-433", "country"));
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);

            // Act & Assert
            Func<Task> act = async () => await _approveOrdersOfficeWorkerHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<UnauthorizedOrderAccessException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidOrderStatus_ShouldThrowCannotChangeOrderStateException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new ApproveOrderOfficeWorker(orderId);

            var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "officeworker",
                true, new Dictionary<string, string>());
            var orderStatus = OrderStatus.Delivered;
            var order = Order.Create(new AggregateId(orderId), Guid.NewGuid(), orderStatus,
                DateTime.Now, "validName", "valid@email.com",
                new Address("street", "buildingNumber", "apartmentNumber", "city", "00-433", "country"));

            _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);
            _orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);

            var cancellationToken = new CancellationToken();


            // Act & Assert
            Func<Task> act = async () => await _approveOrdersOfficeWorkerHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<CannotChangeOrderStateException>()
                .Where(ex => ex.CurrentStatus == orderStatus);
        }
    }
}
