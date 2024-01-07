using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Exceptions;
using SwiftParcel.ExternalAPI.Lecturer.Core.Repositories;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands.Handlers
{
    public class AddParcelHandler: ICommandHandler<AddParcel>
    {
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IInquiresServiceClient _inquiresServiceClient;
        private readonly IInquiryOfferRepository _inquiryOfferRepository;
        private readonly string _dimensionUnit = "Meters";
        private readonly string _currency = "Pln";
        private readonly string _weightUnit = "Kilograms";
        public AddParcelHandler(IIdentityManagerServiceClient identityManagerServiceClient, 
            IInquiresServiceClient inquiresServiceClient, IInquiryOfferRepository inquiryOfferRepository)
        {
            _identityManagerServiceClient = identityManagerServiceClient;
            _inquiresServiceClient = inquiresServiceClient;
            _inquiryOfferRepository = inquiryOfferRepository;
        }
        public async Task HandleAsync(AddParcel command, CancellationToken cancellationToken)
        {
            var token = await _identityManagerServiceClient.GetToken();
            var pickupDate = DateTime.Parse(command.PickupDate);
            var delivaryDate = DateTime.Parse(command.DeliveryDate);
            var inquiry = new InquiryDto(command.Weight, command.Height, command.Depth, _dimensionUnit, _currency, command.Weight,
                _weightUnit, command.SourceBuildingNumber, command.SourceApartmentNumber, command.SourceStreet, command.SourceCity,
                command.SourceZipCode, command.SourceCountry, command.DestinationBuildingNumber, command.DestinationApartmentNumber,
                command.DestinationStreet, command.DestinationCity, command.DestinationZipCode, command.DestinationCountry,
                pickupDate, delivaryDate, command.AtWeekend, command.Priority, command.IsCompany, command.VipPackage);
            var response = await _inquiresServiceClient.PostAsync(token, inquiry);
            if(response == null)
            {
                throw new InquiresServiceConnectionException();
            }
            if (!response.Response.IsSuccessStatusCode)
            {
                throw new InquiresServiceException(response.Response.ReasonPhrase);
            }
            var priceBreakDown = new List<PriceBreakDownItem>();
            foreach (var item in response.Result.PriceBreakDown)
            {
                priceBreakDown.Add(new PriceBreakDownItem(item.Amount, item.Currency, item.Description));
            }

            var inquiryOffer = new InquiryOffer(command.ParcelId, response.Result.InquiryId, 
                response.Result.TotalPrice, response.Result.ExpiringAt, priceBreakDown);
            await _inquiryOfferRepository.AddAsync(inquiryOffer);
        }
    }
}
