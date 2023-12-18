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
using SwiftParcel.Services.Parcels.Application.Services.Clients;

namespace SwiftParcel.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPricingServiceClient _pricingServiceClient;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMessageBroker _messageBroker;
        private readonly string _expectedFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";


        public AddParcelHandler(IParcelRepository parcelRepository, ICustomerRepository customerRepository,
            IPricingServiceClient pricingServiceClient, IDateTimeProvider dateTimeProvider, IMessageBroker messageBroker)
        {
            _parcelRepository = parcelRepository;
            _customerRepository = customerRepository;
            _pricingServiceClient = pricingServiceClient;
            _dateTimeProvider = dateTimeProvider;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(AddParcel command, CancellationToken cancellationToken = default)
        {
            Guid? customerId = command.CustomerId == Guid.Empty ? null : command.CustomerId;
            if (customerId != null && !await _customerRepository.ExistsAsync(command.CustomerId))
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }
            if (!Enum.TryParse<Priority>(command.Priority, true, out var priority))
            {
                throw new InvalidParcelPriorityException(command.Priority);
            }
            if (!DateTime.TryParseExact(command.PickupDate, _expectedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime pickupDate))
            {
                throw new InvalidParcelDateTimeException("pickup_date",command.PickupDate);
            }
            if (!DateTime.TryParseExact(command.DeliveryDate, _expectedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime deliveryDate))
            {
                throw new InvalidParcelDateTimeException("delivery_date", command.DeliveryDate);
            }
            var createdAt = _dateTimeProvider.Now;
            var validTo = createdAt.AddMinutes(30);
            var price = await _pricingServiceClient.GetParcelDeliveryPriceAsync(customerId ?? command.CustomerId, 0.0m, 
            command.Width, command.Height, command.Depth, command.Weight, priority == Priority.High, command.AtWeekend);
            if (price == null)
            {
                throw new PricingServiceException(command.ParcelId);
            }

            var parcel = new Parcel(command.ParcelId, command.Description, command.Width, 
            command.Height, command.Depth, command.Weight, pickupDate, deliveryDate,
            createdAt, price.OrderPrice, validTo, customerId);

            parcel.SetSourceAddress(command.SourceStreet, command.SourceBuildingNumber,
                command.SourceApartmentNumber, command.SourceCity, command.SourceZipCode,
                command.SourceCountry);
            parcel.SetDestinationAddress(command.DestinationStreet, command.DestinationBuildingNumber,
                command.DestinationApartmentNumber, command.DestinationCity, command.DestinationZipCode,
                command.DestinationCountry);

            parcel.SetPriority(priority);

            parcel.SetAtWeekend(command.AtWeekend);

            await _parcelRepository.AddAsync(parcel);

            await _messageBroker.PublishAsync(new ParcelAdded(command.ParcelId));
        }
    }
}
