using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Exceptions;
using SwiftParcel.ExternalAPI.Lecturer.Core.Repositories;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IOffersServiceClient _offersServiceClient;
        private readonly IOfferSnippetRepository _offerSnippetRepository;

        public CreateOrderHandler(IIdentityManagerServiceClient identityManagerServiceClient,
            IOffersServiceClient offersServiceClient, IOfferSnippetRepository offerSnippetRepository)
        {
            _identityManagerServiceClient = identityManagerServiceClient;
            _offersServiceClient = offersServiceClient;
            _offerSnippetRepository = offerSnippetRepository;
        }
        public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken)
        {
            var token = await _identityManagerServiceClient.GetToken();
            var response = await _offersServiceClient.PostAsync(token, new OfferRequestDto
            {
                InquiryId = command.ParcelId,
                Name = command.Name,
                Email = command.Email,
                Address = new InquiryAddressDto
                {
                    HouseNumber = command.Address.BuildingNumber,
                    ApartmentNumber = command.Address.ApartmentNumber,
                    Street = command.Address.Street,
                    City = command.Address.City,
                    Country = command.Address.Country,
                    ZipCode = command.Address.ZipCode
                }
            });
            if(response == null)
            {
                throw new OffersServiceConnectionException();
            }
            if (!response.Response.IsSuccessStatusCode)
            {
                throw new OffersServiceException(response.Response.ReasonPhrase);
            }   

            var offerSnippet = new OfferSnippet(response.Result.OfferRequestId, null, command.CustomerId,
                response.Result.ValidTo, OfferSnippetStatus.WaitingForDecision);

            await _offerSnippetRepository.AddAsync(offerSnippet);
        }
    }
}