using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Parcels.Application.Services;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;
using SwiftParcel.Services.Parcels.Core.Exceptions;

namespace SwiftParcel.Services.Parcels.Application.Commands.Handlers
{
    public class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly IAppContext _appContext;

        public DeleteParcelHandler(IParcelRepository parcelRepository, IAppContext appContext)
        {
            _parcelRepository = parcelRepository;
            _appContext = appContext;
        }

        public async Task HandleAsync(DeleteParcel command, CancellationToken cancellationToken = default)
        {
            var parcel = await _parcelRepository.GetAsync(command.ParcelId);

            if (parcel is null)
            {
                throw new ParcelNotFoundException(command.ParcelId);
            }

            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != parcel.CustomerId && !identity.IsAdmin)
            {
                throw new UnauthorizedParcelAccessException(command.ParcelId, identity.Id);
            }

            await _parcelRepository.DeleteAsync(command.ParcelId);
        }
    }
}
