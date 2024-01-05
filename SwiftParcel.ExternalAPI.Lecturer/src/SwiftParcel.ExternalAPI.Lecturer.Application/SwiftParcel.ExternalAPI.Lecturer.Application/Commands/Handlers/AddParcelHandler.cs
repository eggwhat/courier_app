using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands.Handlers
{
    public class AddParcelHandler: ICommandHandler<AddParcel>
    {
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IInquiriesServiceClient _inquiriesServiceClient;
        public AddParcelHandler(IIdentityManagerServiceClient identityManagerServiceClient, 
            IInquiriesServiceClient inquiriesServiceClient)
        {
            _identityManagerServiceClient = identityManagerServiceClient;
            _inquiriesServiceClient = inquiriesServiceClient;
        }
        public async Task HandleAsync(AddParcel command, CancellationToken cancellationToken)
        {
            var token = await _identityManagerServiceClient.GetToken();
            var dimensionUnit = "Meters";
            var currency = "Pln";
            var weightUnit = "Kilograms";
            var pickupDate = DateTime.Parse(command.PickupDate);
            var delivaryDate = DateTime.Parse(command.DeliveryDate);
            var inquiry = new InquiryDto(command.Weight, command.Height, command.Depth, dimensionUnit, currency, command.Weight,
                weightUnit, command.SourceStreet, command.SourceBuildingNumber, command.SourceApartmentNumber, command.SourceCity,
                command.SourceZipCode, command.SourceCountry, command.DestinationStreet, command.DestinationBuildingNumber,
                command.DestinationApartmentNumber, command.DestinationCity, command.DestinationZipCode, command.DestinationCountry,
                pickupDate, delivaryDate, command.AtWeekend, command.Priority, command.IsCompany, command.VipPackage);
            var response = await _inquiriesServiceClient.PostAsync(token, inquiry);
            if (!response.Response.IsSuccessStatusCode)
            {
                //throw new Exception(response.Error);
            }
        }
    }
}
