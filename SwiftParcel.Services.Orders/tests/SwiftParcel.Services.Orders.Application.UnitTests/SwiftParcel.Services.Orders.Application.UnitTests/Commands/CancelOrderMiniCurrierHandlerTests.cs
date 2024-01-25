using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Commands
{
    public class CancelOrderMiniCurrierHandlerTests
    {
        private readonly CancelOrderMiniCurrierHandler _cancelOrderMiniCurrierHandler;
        private readonly Mock<ILecturerApiServiceClient> _lecturerApiServiceClientMock;

        public CancelOrderMiniCurrierHandlerTests()
        {
            _lecturerApiServiceClientMock = new Mock<ILecturerApiServiceClient>();
            _cancelOrderMiniCurrierHandler = new CancelOrderMiniCurrierHandler(_lecturerApiServiceClientMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithSuccessfulCancellation_ShouldNotThrowException()
        {
            // Arrange
            var cancelCommand = new CancelOrder(Guid.NewGuid(), Company.MiniCurrier);
            var command = new CancelOrderMiniCurrier(cancelCommand);
            var cancellationToken = new CancellationToken();

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            _lecturerApiServiceClientMock.Setup(client => client.PostCancelOrder(command.OrderId.ToString()))
                .ReturnsAsync(response);

            // Act & Assert
            await _cancelOrderMiniCurrierHandler.HandleAsync(command, cancellationToken); // No exception should be thrown
        }

        [Fact]
        public async Task HandleAsync_WithNullResponse_ShouldThrowLecturerApiServiceConnectionException()
        {
            // Arrange
            var cancelCommand = new CancelOrder(Guid.NewGuid(), Company.MiniCurrier);
            var command = new CancelOrderMiniCurrier(cancelCommand);
            var cancellationToken = new CancellationToken();

            _lecturerApiServiceClientMock.Setup(client => client.PostCancelOrder(command.OrderId.ToString()))
                .ReturnsAsync((HttpResponseMessage)null);

            // Act & Assert
            await Assert.ThrowsAsync<LecturerApiServiceConnectionException>(() 
                => _cancelOrderMiniCurrierHandler.HandleAsync(command, cancellationToken));
        }

        [Fact]
        public async Task HandleAsync_WithErrorResponse_ShouldThrowLecturerApiServiceException()
        {
            // Arrange
            var cancelCommand = new CancelOrder(Guid.NewGuid(), Company.MiniCurrier);
            var command = new CancelOrderMiniCurrier(cancelCommand);
            var cancellationToken = new CancellationToken();

            var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = "Bad Request"
            };

            _lecturerApiServiceClientMock.Setup(client => client.PostCancelOrder(command.OrderId.ToString()))
                .ReturnsAsync(errorResponse);

            // Act & Assert
            await Assert.ThrowsAsync<LecturerApiServiceException>(()
                => _cancelOrderMiniCurrierHandler.HandleAsync(command, cancellationToken));
        }
    }
}
