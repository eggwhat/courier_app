using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.Events;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
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
            var decisionDate = DateTime.Now;
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(decisionDate);

            var cancellationToken = new CancellationToken();

            // Act
            await _approveOrdersOfficeWorkerHandler.HandleAsync(command, cancellationToken);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.UpdateAsync(order), Times.Once);
            _eventMapperMock.Verify(mapper => mapper.MapAll(order.Events), Times.Once);
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<SendApprovalEmail>(), cancellationToken), Times.Once);
        }

    //     [Fact]
    //     public async Task HandleAsync_WithInvalidOrder_ShouldThrowOrderNotFoundException()
    //     {
    //         // Arrange
    //         var command = new ApproveOrderOfficeWorker
    //         {
    //             OrderId = Guid.NewGuid()
    //             // Set other properties as needed
    //         };

    //         var cancellationToken = new CancellationToken();
    //         var orderRepositoryMock = new Mock<IOrderRepository>();
    //         var appContextMock = new Mock<IAppContext>();
    //         var dateTimeProviderMock = new Mock<IDateTimeProvider>();
    //         var eventMapperMock = new Mock<IEventMapper>();
    //         var messageBrokerMock = new Mock<IMessageBroker>();
    //         var commandDispatcherMock = new Mock<ICommandDispatcher>();

    //         var handler = new ApproveOrderOfficeWorkerHandler(
    //             orderRepositoryMock.Object,
    //             appContextMock.Object,
    //             dateTimeProviderMock.Object,
    //             eventMapperMock.Object,
    //             messageBrokerMock.Object,
    //             commandDispatcherMock.Object
    //         );

    //         orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync((Order)null);

    //         // Act & Assert
    //         await Assert.ThrowsAsync<OrderNotFoundException>(() => handler.HandleAsync(command, cancellationToken));
    //     }

    //     [Fact]
    //     public async Task HandleAsync_WithNonOfficeWorkerIdentity_ShouldThrowUnauthorizedOrderAccessException()
    //     {
    //         // Arrange
    //         var orderId = Guid.NewGuid();
    //         var command = new ApproveOrderOfficeWorker(orderId);

    //         var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "officeworker",
    //             true, new Dictionary<string, string>());

    //         _appContextMock.Setup(ctx => ctx.Identity).Returns(identityContext);



    //         var order = new Order
    //         {
    //             Id = command.OrderId,
    //             // Set other properties as needed
    //         };

    //         var identity = new AppIdentity
    //         {
    //             IsOfficeWorker = false
    //             // Set other identity properties as needed
    //         };

    //         appContextMock.Setup(ctx => ctx.Identity).Returns(identity);
    //         orderRepositoryMock.Setup(repo => repo.GetAsync(command.OrderId)).ReturnsAsync(order);

    //         // Act & Assert
    //         await Assert.ThrowsAsync<UnauthorizedOrderAccessException>(() => handler.HandleAsync(command, cancellationToken));
    //     }
    }

    
}
