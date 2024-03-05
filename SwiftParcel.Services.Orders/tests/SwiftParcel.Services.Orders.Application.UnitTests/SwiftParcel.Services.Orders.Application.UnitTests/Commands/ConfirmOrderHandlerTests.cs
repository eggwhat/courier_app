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
    public class ConfirmOrderHandlerTests
    {
        private readonly ConfirmOrderHandler _confirmOrderHandler;
        private readonly Mock<ICommandDispatcher> _commandDispatcherMock;

        public ConfirmOrderHandlerTests()
        {
            _commandDispatcherMock = new Mock<ICommandDispatcher>();
            _confirmOrderHandler = new ConfirmOrderHandler(_commandDispatcherMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithSwiftParcelCompany_ShouldSendConfirmOrderSwiftParcelCommand()
        {
            // Arrange
            var command = new ConfirmOrder(Guid.NewGuid(), Company.SwiftParcel);
            var cancellationToken = new CancellationToken();

            // Act
            await _confirmOrderHandler.HandleAsync(command, cancellationToken);

            // Assert
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<ConfirmOrderSwiftParcel>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithMiniCurrierCompany_ShouldSendCancelOrderMiniCurrierCommand()
        {
            // Arrange
            var command = new ConfirmOrder(Guid.NewGuid(), Company.MiniCurrier);
            var cancellationToken = new CancellationToken();

            // Act
            await _confirmOrderHandler.HandleAsync(command, cancellationToken);

            // Assert
            _commandDispatcherMock.Verify(dispatcher => dispatcher.SendAsync(It.IsAny<ConfirmOrderMiniCurrier>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidCompany_ShouldThrowCompanyNotFoundException()
        {
            // Arrange
            var command = new ConfirmOrder(Guid.NewGuid(), (Company)int.MaxValue);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            Func<Task> act = async () => await _confirmOrderHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<CompanyNotFoundException>();
        }
    }
}
