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
    public class ConfirmOrderMiniCurrierHandlerTests
    {
        private readonly ConfirmOrderMiniCurrierHandler _confirmOrderMiniCurrierHandler;
        private readonly Mock<ILecturerApiServiceClient> _lecturerApiServiceClientMock;

        public ConfirmOrderMiniCurrierHandlerTests()
        {
            _lecturerApiServiceClientMock = new Mock<ILecturerApiServiceClient>();
            _confirmOrderMiniCurrierHandler = new ConfirmOrderMiniCurrierHandler(_lecturerApiServiceClientMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithSuccessfulConfirmation_ShouldNotThrowException()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.MiniCurrier);
            var command = new ConfirmOrderMiniCurrier(confirmCommand);

            var cancellationToken = new CancellationToken();

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            _lecturerApiServiceClientMock.Setup(client => client.PostConfirmOrder(command.OrderId.ToString()))
                .ReturnsAsync(response);

            // Act & Assert
            await _confirmOrderMiniCurrierHandler.HandleAsync(command, cancellationToken); // No exception should be thrown
        }

        [Fact]
        public async Task HandleAsync_WithNullResponse_ShouldThrowLecturerApiServiceConnectionException()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.MiniCurrier);
            var command = new ConfirmOrderMiniCurrier(confirmCommand);

            var cancellationToken = new CancellationToken();

            _lecturerApiServiceClientMock.Setup(client => client.PostConfirmOrder(command.OrderId.ToString()))
                .ReturnsAsync((HttpResponseMessage)null);

            // Act & Assert
            await Assert.ThrowsAsync<LecturerApiServiceConnectionException>(() 
                => _confirmOrderMiniCurrierHandler.HandleAsync(command, cancellationToken));
        }

        [Fact]
        public async Task HandleAsync_WithErrorResponse_ShouldThrowLecturerApiServiceException()
        {
            // Arrange
            var confirmCommand = new ConfirmOrder(Guid.NewGuid(), Company.MiniCurrier);
            var command = new ConfirmOrderMiniCurrier(confirmCommand);

            var cancellationToken = new CancellationToken();
            var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = "Bad Request"
            };

            _lecturerApiServiceClientMock.Setup(client => client.PostConfirmOrder(command.OrderId.ToString()))
                .ReturnsAsync(errorResponse);

            // Act & Assert
            await Assert.ThrowsAsync<LecturerApiServiceException>(() 
                => _confirmOrderMiniCurrierHandler.HandleAsync(command, cancellationToken));
        }
    }
}
