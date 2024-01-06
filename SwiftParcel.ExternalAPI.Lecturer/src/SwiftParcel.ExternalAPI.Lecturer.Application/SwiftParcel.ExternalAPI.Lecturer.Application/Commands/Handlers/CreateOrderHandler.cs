using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IOffersServiceClient _offersServiceClient;

        public CreateOrderHandler(IIdentityManagerServiceClient identityManagerServiceClient,
            IOffersServiceClient offersServiceClient)
        {
            _identityManagerServiceClient = identityManagerServiceClient;
            _offersServiceClient = offersServiceClient;
        }
        public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken)
        {
            var token = await _identityManagerServiceClient.GetToken();
            var response = await _offersServiceClient.PostAsync(token, new OfferDto
            {
                InquiryId = command.ParcelId,
                Name = command.Name,
                Email = command.Email,
                Address = new AddressDto
                {
                    BuildingNumber = command.Address.BuildingNumber,
                    ApartmentNumber = command.Address.ApartmentNumber,
                    Street = command.Address.Street,
                    City = command.Address.City,
                    Country = command.Address.Country,
                    ZipCode = command.Address.ZipCode
                }
            });
            if(response == null)
            {
                //throw new Exception("Offer was not created");
            }
            if (!response.Response.IsSuccessStatusCode)
            {
                //throw new Exception("Offer was not created");
            }

        }
    }
}