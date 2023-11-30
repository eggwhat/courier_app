using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Parcels.Application.Events;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Application.Services;
using SwiftParcel.Services.Parcels.Core.Repositories;

namespace SwiftParcel.Services.Parcels.Application.Commands.Handlers
{
    public class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly IParcelRepository ParcelRepository;
        private readonly IAppContext AppContext;
        private readonly IMessageBroker MessageBroker;

        public DeleteParcelHandler(IParcelRepository parcelRepository, IAppContext appContext, IMessageBroker messageBroker)
        {
            ParcelRepository = parcelRepository;
            AppContext = appContext;
            MessageBroker = messageBroker;
        }

        public async Task HandleAsync(DeleteParcel command, CancellationToken cancellationToken = default)
        {
            var parcel = await ParcelRepository.GetAsync(command.ParcelId);

            if (parcel is null)
            {
                throw new ParcelNotFoundException(command.ParcelId);
            }

            if (parcel.OrderId.HasValue)
            {
                throw new CannotDeleteParcelException(command.ParcelId);
            }

            var identity = AppContext.Identity;
            if (identity.IsAuthenticated && identity.Id != parcel.CustomerId && !identity.IsAdmin)
            {
                throw new UnauthorizedParcelAccessException(command.ParcelId, identity.Id);
            }

            await ParcelRepository.DeleteAsync(command.ParcelId);

            await MessageBroker.PublishAsync(new ParcelDeleted(command.ParcelId));
        }
    }
}
