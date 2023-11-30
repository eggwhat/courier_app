using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Parcels.Application.Services;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;
using SwiftParcel.Services.Parcels.Core.Exceptions;

namespace SwiftParcel.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository _parcelRepository;

        public AddParcelHandler(IParcelRepository parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }

        public async Task HandleAsync(AddParcel command, CancellationToken cancellationToken = default)
        {
            var parcel = new Parcel(command.ParcelId, command.OrderId, command.CustomerId, command.Description,
                command.Width, command.Height, command.Depth, command.Weight, command.Price);

            await _parcelRepository.AddAsync(parcel);
        }
    }
}
