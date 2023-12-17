using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Parcels.Application.Events;
using SwiftParcel.Services.Parcels.Application.Exceptions;
using SwiftParcel.Services.Parcels.Application.Services;
using SwiftParcel.Services.Parcels.Core.Entities;
using SwiftParcel.Services.Parcels.Core.Repositories;
using SwiftParcel.Services.Parcels.Core.Exceptions;

namespace SwiftParcel.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository ParcelRepository;
        private readonly ICustomerRepository CustomerRepository;
        private readonly IDateTimeProvider DateTimeProvider;
        private readonly IMessageBroker MessageBroker;
        private readonly string expectedFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";


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
            Guid? customerId = command.CustomerId == Guid.Empty ? null : command.CustomerId;
            if (customerId != null && !await CustomerRepository.ExistsAsync(command.CustomerId))
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }
            if (!Enum.TryParse<Priority>(command.Priority, true, out var priority))
            {
                throw new InvalidParcelPriorityException(command.Priority);
            }
            if (!DateTime.TryParseExact(command.PickupDate, expectedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime pickupDate))
            {
                throw new InvalidParcelDateTimeException("pickup_date",command.PickupDate);
            }
            if (!DateTime.TryParseExact(command.DeliveryDate, expectedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime deliveryDate))
            {
                throw new InvalidParcelDateTimeException("delivery_date", command.DeliveryDate);
            }
            var parcel = new Parcel(command.ParcelId, command.Description, command.Width, 
            command.Height, command.Depth, command.Weight, pickupDate, deliveryDate,
            DateTimeProvider.Now, customerId, null, null);

            parcel.SetSourceAddress(command.SourceStreet, command.SourceBuildingNumber,
                command.SourceApartmentNumber, command.SourceCity, command.SourceZipCode,
                command.SourceCountry);
            parcel.SetDestinationAddress(command.DestinationStreet, command.DestinationBuildingNumber,
                command.DestinationApartmentNumber, command.DestinationCity, command.DestinationZipCode,
                command.DestinationCountry);

            parcel.SetPriority(priority);

            parcel.SetAtWeekend(command.AtWeekend);

            await ParcelRepository.AddAsync(parcel);

            await MessageBroker.PublishAsync(new ParcelAdded(command.ParcelId));
        }
    }
}
