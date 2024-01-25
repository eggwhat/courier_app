using SwiftParcel.Services.Orders.Application.Events.External;
using SwiftParcel.Services.Orders.Application.Events.External.Handlers;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Repositories;

namespace SwiftParcel.Services.Orders.Application.UnitTests
{
    public class CustomerCreatedHandlerTests
    {
        private readonly CustomerCreatedHandler _customerCreatedHandler;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public CustomerCreatedHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerCreatedHandler = new CustomerCreatedHandler(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithCustomerAlreadyAdded_ShouldThrowCustomerAlreadyAddedException()
        {
            // Arrange
            var @event = new CustomerCreated(Guid.NewGuid(), "email", "fullName");
            var cancellationToken = new CancellationToken();

            _customerRepositoryMock.Setup(repository => repository.ExistsAsync(@event.CustomerId))
                .ReturnsAsync(true);

            // Act & Assert
            Func<Task> act = async () => await _customerCreatedHandler.HandleAsync(@event, cancellationToken);
            await act.Should().ThrowAsync<CustomerAlreadyAddedException>();
        }

        [Fact]
        public async Task HandleAsync_WithCustomerNotAdded_ShouldAddCustomer()
        {
            // Arrange
            var @event = new CustomerCreated(Guid.NewGuid(), "email", "fullName");
            var cancellationToken = new CancellationToken();

            _customerRepositoryMock.Setup(repository => repository.ExistsAsync(@event.CustomerId))
                .ReturnsAsync(false);

            // Act
            await _customerCreatedHandler.HandleAsync(@event, cancellationToken);

            // Assert
            _customerRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Customer>()), Times.Once);
        }
    }
}
