using Castle.Core.Resource;
using SwiftParcel.Services.Orders.Application.Commands;
using SwiftParcel.Services.Orders.Application.Commands.Handlers;
using SwiftParcel.Services.Orders.Application.DTO;
using SwiftParcel.Services.Orders.Application.Events;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Events;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace SwiftParcel.Services.Orders.Application.UnitTests.Commands
{
    public class CreateOrderSwiftParcelHandlerTests
    {
        private readonly CreateOrderSwiftParcelHandler _createOrderSwiftParcelHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly Mock<IEventMapper> _eventMapperMock;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

        public CreateOrderSwiftParcelHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _eventMapperMock = new Mock<IEventMapper>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _createOrderSwiftParcelHandler = new CreateOrderSwiftParcelHandler(_orderRepositoryMock.Object, _customerRepositoryMock.Object,
                _messageBrokerMock.Object, _eventMapperMock.Object, _dateTimeProviderMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithValidParcelId_ShouldCreateOrderAndPublishEvents()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "32",
                BuildingNumber = "32",
                ZipCode = "00-323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "valid@email.com", address, Company.SwiftParcel);
            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);

            
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);
            var order = Order.Create(command.OrderId, command.CustomerId, OrderStatus.WaitingForDecision, dateTimeNow,
                               command.Name, command.Email, command.Address);

            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidParcelValidToDate_ShouldThrowParcelRequestExpiredException()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "32",
                BuildingNumber = "32",
                ZipCode = "00-323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "valid@email.com", address, Company.SwiftParcel);
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(-1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);

            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(DateTime.Now);

            // Act & Assert
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<ParcelRequestExpiredException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidAddressFieldCity_ShouldThrowInvalidAddressElementException()
        {
            // Arrange
            var address = new Address()
            {
                City = "",
                Country = "Country",
                ApartmentNumber = "32",
                BuildingNumber = "32",
                ZipCode = "00-323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "valid@email.com", address, Company.SwiftParcel);

            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);
            
            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<InvalidAddressElementException>().Where(ex => ex.AddressElement == "city");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidAddressFieldCountry_ShouldThrowInvalidAddressElementException()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "",
                ApartmentNumber = "32",
                BuildingNumber = "32",
                ZipCode = "00-323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "valid@email.com", address, Company.SwiftParcel);

            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);

            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<InvalidAddressElementException>().Where(ex => ex.AddressElement == "country");
        }


        [Fact]
        public async Task HandleAsync_WithInvalidAddressFieldBuildingNumber_ShouldThrowInvalidAddressElementException()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "",
                BuildingNumber = "",
                ZipCode = "00-323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "valid@email.com", address, Company.SwiftParcel);

            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);

            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<InvalidAddressElementException>().Where(ex => ex.AddressElement == "building number");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidAddressFieldZipCode_ShouldThrowInvalidAddressElementException()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "",
                BuildingNumber = "32",
                ZipCode = "00323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "valid@email.com", address, Company.SwiftParcel);

            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);

            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<InvalidAddressElementException>().Where(ex => ex.AddressElement == "zip code");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidAddressFieldStreet_ShouldThrowInvalidAddressElementException()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "",
                BuildingNumber = "",
                ZipCode = "00-323",
                Street = ""
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "valid@email.com", address, Company.SwiftParcel);

            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);

            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<InvalidAddressElementException>().Where(ex => ex.AddressElement == "street");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidBuyerName_ShouldThrowInvalidBuyerNameException()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "",
                BuildingNumber = "",
                ZipCode = "00-323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "", "valid@email.com", address, Company.SwiftParcel);

            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);

            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<InvalidBuyerNameException>().Where(ex => ex.BuyerName == "");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidBuyerEmail_ShouldThrowInvalidBuyerEmailException()
        {
            // Arrange
            var address = new Address()
            {
                City = "City",
                Country = "Country",
                ApartmentNumber = "",
                BuildingNumber = "3",
                ZipCode = "00-323",
                Street = "Street"
            };
            var cancellationToken = new CancellationToken();
            var createOrderCommand = new CreateOrder(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                    "Name", "invalidemail.com", address, Company.SwiftParcel);

            var dateTimeNow = DateTime.Now;
            var parcelDto = new ParcelDto()
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Description = "Sample Description",
                Width = 0.2,
                Height = 0.2,
                Depth = 0.3,
                Weight = 0.5,
                Source = new AddressDto("", "", "", "", "", ""),
                Destination = new AddressDto("", "", "", "", "", ""),
                Priority = Priority.Low,
                AtWeekend = false,
                PickupDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                IsCompany = false,
                VipPackage = false,
                CreatedAt = DateTime.Now,
                ValidTo = dateTimeNow.AddDays(1),
                CalculatedPrice = 50.0m, // Set a sample price
                PriceBreakDown = new List<PriceBreakDownItemDto>()
            };
            var command = new CreateOrderSwiftParcel(createOrderCommand, parcelDto);
            _dateTimeProviderMock.Setup(provider => provider.Now).Returns(dateTimeNow);

            // Act
            Func<Task> act = async () => await _createOrderSwiftParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<InvalidBuyerEmailException>();
        }
    }
}
