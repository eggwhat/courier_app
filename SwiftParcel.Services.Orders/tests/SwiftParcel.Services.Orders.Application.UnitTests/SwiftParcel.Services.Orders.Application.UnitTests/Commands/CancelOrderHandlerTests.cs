using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Commands
{
    public class CancelOrderHandlerTests
    {
        private readonly CancelOrderHandler _cancelOrderHandler;
        private readonly Mock<ICommandDispatcher> _commandDispatcherMock;

        public CancelOrderHandlerTests()
        {
            _commandDispatcherMock = new Mock<ICommandDispatcher>();
            _cancelOrderHandler = new CancelOrderHandler(_commandDispatcherMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithSwiftParcelCompany_ShouldSendCancelOrderSwiftParcelCommand()
        {
            // Arrange
            var command = new CancelOrder(Guid.NewGuid(), Company.SwiftParcel);
            var cancellationToken = new CancellationToken();

            // Act
            await _cancelOrderHandler.HandleAsync(command, cancellationToken);

            // Assert
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<CancelOrderSwiftParcel>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithMiniCurrierCompany_ShouldSendCancelOrderMiniCurrierCommand()
        {
            // Arrange
            var command = new CancelOrder(Guid.NewGuid(), Company.MiniCurrier);
            var cancellationToken = new CancellationToken();

            // Act
            await _cancelOrderHandler.HandleAsync(command, cancellationToken);

            // Assert
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<CancelOrderMiniCurrier>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidCompany_ShouldThrowCompanyNotFoundException()
        {
            // Arrange
            var command = new CancelOrder(Guid.NewGuid(), (Company)int.MaxValue);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            Func<Task> act = async () => await _cancelOrderHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<CompanyNotFoundException>();
        }
    }
}
