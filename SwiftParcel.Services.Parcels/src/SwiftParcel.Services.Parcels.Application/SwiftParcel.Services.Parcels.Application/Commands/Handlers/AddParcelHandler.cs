using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Parcels.Application.Events;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Application.Services;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;
using System.Drawing;

namespace SwiftParcel.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository ParcelRepository;
        private readonly ICustomerRepository CustomerRepository;
        private readonly IDateTimeProvider DateTimeProvider;
        private readonly IMessageBroker MessageBroker;

        public AddParcelHandler(IParcelRepository parcelRepository, ICustomerRepository customerRepository,
            IDateTimeProvider dateTimeProvider, IMessageBroker messageBroker)
        {
            ParcelRepository = parcelRepository;
            CustomerRepository = customerRepository;
            DateTimeProvider = dateTimeProvider;
            MessageBroker = messageBroker;
        }

        public async Task HandleAsync(AddParcel command, CancellationToken cancellationToken = default)
        {
            if (!(await CustomerRepository.ExistsAsync(command.CustomerId)))
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            if (!Enum.TryParse<Variant>(command.Variant, true, out var variant))
            {
                throw new InvalidParcelVariantException(command.Variant);
            }

            if (!Enum.TryParse<Priority>(command.Priority, true, out var priority))
            {
                throw new InvalidParcelPriorityException(command.Priority);
            }

            var parcel = new Parcel(command.ParcelId, command.CustomerId, command.Name, command.Description,
                command.Width, command.Height, command.Depth, command.Weight, command.Price, DateTimeProvider.Now);

            parcel.SetSourceAddress(command.SourceStreet, command.SourceBuildingNumber,
                command.SourceApartmentNumber, command.SourceCity, command.SourceZipCode);
            parcel.SetDestinationAddress(command.DestinationStreet, command.DestinationBuildingNumber,
                command.DestinationApartmentNumber, command.DestinationCity, command.DestinationZipCode);

            parcel.SetVariant(variant);
            parcel.SetPriority(priority);

            parcel.SetAtWeekend(command.AtWeekend);
            parcel.SetIsFragile(command.IsFragile);

            await ParcelRepository.AddAsync(parcel);

            await MessageBroker.PublishAsync(new ParcelAdded(command.ParcelId));
        }
    }
}
