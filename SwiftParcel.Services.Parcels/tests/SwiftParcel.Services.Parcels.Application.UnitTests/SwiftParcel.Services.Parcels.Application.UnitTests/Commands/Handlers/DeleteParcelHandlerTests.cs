using Castle.Core.Resource;
using Convey.Discovery.Consul.Models;
using SwiftParcel.Services.Parcels.Application.Commands;
using SwiftParcel.Services.Parcels.Application.Commands.Handlers;
using SwiftParcel.Services.Parcels.Application.Events;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Application.Services;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;
using SwiftParcel.Services.Parcels.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.UnitTests.Commands.Handlers
{
    public class DeleteOrderHandlerTests
    {
        private readonly DeleteParcelHandler _deleteParcelHandler;
        private readonly Mock<IParcelRepository> _parcelRepositoryMock;
        private readonly Mock<IAppContext> _appContextMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;

        public DeleteOrderHandlerTests()
        {
            _parcelRepositoryMock = new Mock<IParcelRepository>();
            _appContextMock = new Mock<IAppContext>();
            _messageBrokerMock = new Mock<IMessageBroker>();
            _deleteParcelHandler = new DeleteParcelHandler(_parcelRepositoryMock.Object,
                _appContextMock.Object, _messageBrokerMock.Object);
        }

        [Fact]
        public async Task HandleAsync_WithValidParcelId_ShouldDeleteParcelAndPublishEvent()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var command = new DeleteParcel(parcelId);

            var cancellationToken = new CancellationToken();
            var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "officeworker",
                true, new Dictionary<string, string>());
            var existingParcel = new Parcel(new AggregateId(Guid.NewGuid()), "desc", 0.2, 0.2, 0.2, 0.5,
                Priority.Low, true, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), true, true, DateTime.Now, 0.0m,
                new List<PriceBreakDownItem>(), DateTime.Now, null);

            _appContextMock.Setup(context => context.Identity).Returns(identityContext);
            _parcelRepositoryMock.Setup(repo => repo.GetAsync(command.ParcelId)).ReturnsAsync(existingParcel);

            // Act
            await _deleteParcelHandler.HandleAsync(command, cancellationToken);

            // Assert
            _parcelRepositoryMock.Verify(repo => repo.DeleteAsync(command.ParcelId), Times.Once);
            _messageBrokerMock.Verify(broker => broker.PublishAsync(It.IsAny<ParcelDeleted>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithNonExistingParcelId_ShouldThrowParcelNotFoundException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var command = new DeleteParcel(parcelId);

            var cancellationToken = new CancellationToken();

            _parcelRepositoryMock.Setup(repo => repo.GetAsync(command.ParcelId)).ReturnsAsync((Parcel?)null); // Non-existing parcel

            // Act & Assert
            _parcelRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _messageBrokerMock.Verify(broker => broker.PublishAsync(It.IsAny<ParcelDeleted>()), Times.Never);
            Func<Task> act = async () => await _deleteParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<ParcelNotFoundException>();
        }

        [Fact]
        public async Task HandleAsync_WithFailedAuthorization_ShouldThrowUnauthorizedParcelAccessException()
        {
            // Arrange
            var parcelId = Guid.NewGuid();
            var command = new DeleteParcel(parcelId);

            var cancellationToken = new CancellationToken();
            var identityContext = new IdentityContext(Guid.NewGuid().ToString(), "user",
                true, new Dictionary<string, string>());
            var existingParcel = new Parcel(new AggregateId(Guid.NewGuid()), "desc", 0.2, 0.2, 0.2, 0.5,
                Priority.Low, true, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), true, true, DateTime.Now, 0.0m,
                new List<PriceBreakDownItem>(), DateTime.Now, null);

            _appContextMock.Setup(context => context.Identity).Returns(identityContext);
            _parcelRepositoryMock.Setup(repo => repo.GetAsync(command.ParcelId)).ReturnsAsync(existingParcel) ; // Non-existing parcel

            // Act & Assert
            _parcelRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _messageBrokerMock.Verify(broker => broker.PublishAsync(It.IsAny<ParcelDeleted>()), Times.Never);
            Func<Task> act = async () => await _deleteParcelHandler.HandleAsync(command, cancellationToken);
            await act.Should().ThrowAsync<UnauthorizedParcelAccessException>();
        }
    }
}
