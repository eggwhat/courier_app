using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Commands
{
    public class CreateOrderMiniCurrierHandlerTests
    {
        private readonly CreateOrderMiniCurrierHandler _createOrderMiniCurrierHandler;
        private readonly Mock<ILecturerApiServiceClient> _lecturerApiServiceClientMock;

        public CreateOrderMiniCurrierHandlerTests()
        {
            _lecturerApiServiceClientMock = new Mock<ILecturerApiServiceClient>();
            _createOrderMiniCurrierHandler = new CreateOrderMiniCurrierHandler(_lecturerApiServiceClientMock.Object);
        }

        //[Fact]
        //public async Task HandleAsync_WithValidOrder_ShouldSendPostOfferAsync()
        //{
        //    // Arrange
        //    var address = new Address()
        //    {
        //        City = "City",
        //        Country = "Country",
        //        ApartmentNumber = "32",
        //        BuildingNumber = "32",
        //        ZipCode = "00-323",
        //        Street = "Street"
        //    };
        //    var createOrder = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Name", "valid@email.com", address, Company.MiniCurrier);
        //    var command = new CreateOrderMiniCurrier(createOrder);
        //    var cancellationToken = new CancellationToken();

        //    // Act
        //    await _createOrderMiniCurrierHandler.HandleAsync(command, cancellationToken);

        //    // Assert
        //    _lecturerApiServiceClientMock.Verify(client => client.PostOfferAsync(It.IsAny<CreateOrderMiniCurrier>()), Times.Once);
        //}

    }
}
