using SwiftParcel.Services.Parcels.Application.Commands;
using SwiftParcel.Services.Parcels.Application.Commands.Handlers;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Application.Services;
using SwiftParcel.Services.Parcels.Application.Services.Clients;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Exceptions;
using SwiftParcel.Services.Parcels.Core.Repositories;

namespace SwiftParcel.Services.Parcels.Application.UnitTests
{
    public class AddParcelHandlerTests
    {
        private readonly AddParcelHandler _addParcelHandler;
        private readonly Mock<IParcelRepository> _parcelRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IPricingServiceClient> _pricingServiceClientMock;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly Mock<ILecturerApiServiceClient> _lecturerApiServiceClientMock;
        private readonly Mock<IBaronomatApiServiceClient> _baronomatApiServiceClientMock;

        public AddParcelHandlerTests()
        {
            _parcelRepositoryMock = new Mock<IParcelRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _pricingServiceClientMock = new Mock<IPricingServiceClient>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _lecturerApiServiceClientMock = new Mock<ILecturerApiServiceClient>();
            _baronomatApiServiceClientMock = new Mock<IBaronomatApiServiceClient>();
            _addParcelHandler = new AddParcelHandler(_parcelRepositoryMock.Object, _customerRepositoryMock.Object,
                               _pricingServiceClientMock.Object, _dateTimeProviderMock.Object, _messageBrokerMock.Object,
                               _lecturerApiServiceClientMock.Object, _baronomatApiServiceClientMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithNotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5, 
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString(), deliveryDate.ToString(), true, true);

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<CustomerNotFoundException>();            
        }

        [Fact]
        public async Task HandleAsync_WithInvalidPriorityValue_ThrowsInvalidParcelPriorityException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                "invalidPriority", true, pickupDate.ToString(), deliveryDate.ToString(), true, true);

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelPriorityException>()
                .Where(ex => ex.Priority == command.Priority);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidPickupDateFormat_ThrowsInvalidParcelDateTimeException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, "invalidDateTime", 
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelDateTimeException>()
                .Where(ex => ex.Value == command.PickupDate && ex.Element == "pickup_date");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDeliveryDateFormat_ThrowsInvalidParcelDateTimeException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddSeconds(10);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), 
                "invalidDateTime", true, true);

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);


            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelDateTimeException>()
                .Where(ex => ex.Value == command.DeliveryDate && ex.Element == "delivery_date");
        }

        [Fact]
        public async Task HandleAsync_WithNullPrice_ThrowsPricingServiceException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), 
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(), 
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), true, false, true))
                .ReturnsAsync((ParcelDeliveryPricingDto?)null);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<PricingServiceException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDescription_ThrowsInvalidParcelDescriptionException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                CustomerDiscount = 0.0m,
                OrderDiscountPrice = 0.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelDescriptionException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidWidth_ThrowsInvalidParcelDimensionException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var invalidDimensionValue = 200;
            var command = new AddParcel(parcelId, customerId, "desc", invalidDimensionValue, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelDimensionException>()
                .Where(ex => ex.DimensionType == "width" && ex.DimensionValue == 200);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidHeight_ThrowsInvalidParcelDimensionException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var invalidDimensionValue = 200;
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, invalidDimensionValue, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelDimensionException>()
                .Where(ex => ex.DimensionType == "height" && ex.DimensionValue == 200);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDepth_ThrowsInvalidParcelDimensionException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var invalidDimensionValue = 200;
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, invalidDimensionValue, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelDimensionException>()
                .Where(ex => ex.DimensionType == "depth" && ex.DimensionValue == 200);
        }

        [Fact]
        public async Task HandleAsync_WithInvalidWeight_ThrowsInvalidParcelWeightException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 1000,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelWeightException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidPickupDate_ThrowsInvalidParcelPickupDateException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);
            _dateTimeProviderMock.Setup(x => x.Now).Returns(DateTime.Now.AddMinutes(100));

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelPickupDateException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDeliveryDate_ThrowsInvalidParcelDeliveryDateException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now;
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidParcelDeliveryDateException>();
        }

        [Fact]
        public async Task HandleAsync_WithInvalidSourceAddressStreet_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "street");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidSourceBuildingNumber_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "building number");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidSourceCity_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "city");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidSourceZipCode_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "invalidZipCode", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "zip code");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidSourceCountry_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "country");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDestinationStreet_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "street");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDestinationBuildingNumber_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "building number");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDestinationCity_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-343", "country",
                "street", "12", "23", "", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "city");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDestinationZipCode_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-323", "country",
                "street", "12", "23", "city", "invalidZipCode", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "zip code");
        }

        [Fact]
        public async Task HandleAsync_WithInvalidDestinationCountry_ThrowsInvalidAddressElementException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "invalidZipCode", "country",
                "street", "12", "23", "city", "00-343", "",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act & Assert
            Func<Task> act = async () => await _addParcelHandler.HandleAsync(command);
            await act.Should().ThrowAsync<InvalidAddressElementException>()
                .Where(ex => ex.AddressElement == "zip code");
        }

        [Fact]
        public async Task HandleAsync_WithValidParcelFields_AddsParcelToRepositoryAndSendsRequestToApis()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickupDate = DateTime.Now.AddMinutes(20);
            var deliveryDate = DateTime.Now.AddDays(1);
            var command = new AddParcel(parcelId, customerId, "desc", 0.2, 0.2, 0.2, 0.5,
                "street", "12", "23", "city", "00-3232", "country",
                "street", "12", "23", "city", "00-343", "country",
                Priority.Low.ToString(), true, pickupDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                deliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), true, true);
            var parcelDeliveryPricing = new ParcelDeliveryPricingDto
            {
                OrderPrice = 10.0m,
                FinalPrice = 10.0m,
                PriceBreakDown = new List<PriceBreakDownItem>() { new PriceBreakDownItem(10.0m, "Pln", "desc") }
            };

            _customerRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _pricingServiceClientMock.Setup(x => x.GetParcelDeliveryPriceAsync(It.IsAny<Guid>(), It.IsAny<decimal>(),
                It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), false, true, true))
                .ReturnsAsync(parcelDeliveryPricing);

            // Act
            await _addParcelHandler.HandleAsync(command);

            // Assert
            _parcelRepositoryMock.Verify(x => x.AddAsync(It.Is<Parcel>(p => p.Id == parcelId &&
                           p.CustomerId == customerId && p.Description == command.Description &&
                           p.Weight == command.Weight && p.Width == command.Width && p.Height == command.Height &&
                           p.Depth == command.Depth && p.Source.Street == command.SourceStreet &&
                           p.Source.BuildingNumber == command.SourceBuildingNumber &&
                           p.Source.ApartmentNumber == command.SourceApartmentNumber &&
                           p.Source.City == command.SourceCity && p.Source.ZipCode == command.SourceZipCode &&
                           p.Source.Country == command.SourceCountry &&
                           p.Destination.Street == command.DestinationStreet &&
                           p.Destination.BuildingNumber == command.DestinationBuildingNumber &&
                           p.Destination.ApartmentNumber == command.DestinationApartmentNumber &&
                           p.Destination.City == command.DestinationCity && p.Destination.ZipCode == command.DestinationZipCode &&
                           p.Destination.Country == command.DestinationCountry &&
                           p.Priority.ToString() == command.Priority && p.VipPackage == command.VipPackage && p.AtWeekend == command.AtWeekend &&
                           p.IsCompany == command.IsCompany)), Times.Once);

        }
    }
}