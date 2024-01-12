using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Core.Repositories;
using SwiftParcel.ExternalAPI.Lecturer.Application.Exceptions;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands.Handlers
{
    public class ConfirmOrderHandler : ICommandHandler<ConfirmOrder>
    {
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IOfferSnippetRepository _offerSnippetRepository;
        private readonly IOffersServiceClient _officeServiceClient;

        public ConfirmOrderHandler(IIdentityManagerServiceClient identityManagerServiceClient, 
            IOfferSnippetRepository offerSnippetRepository, IOffersServiceClient officeServiceClient)
        {
            _identityManagerServiceClient = identityManagerServiceClient;
            _offerSnippetRepository = offerSnippetRepository;
            _officeServiceClient = officeServiceClient;
        }

        public async Task HandleAsync(ConfirmOrder command, CancellationToken cancellationToken)
        {
            var offer = await _offerSnippetRepository.GetAsync(command.OrderId);
            if (offer is null)
            {
                throw new OfferNotFoundException(command.OrderId);
            }
            if(offer.Status != OfferSnippetStatus.Approved)
            {
                throw new OfferNotApprovedException(command.OrderId, offer.Status);
            }

            var token = await _identityManagerServiceClient.GetToken();
            var response = await _officeServiceClient.PostConfirmOffer(token, offer.OfferId.ToString());
            if(response is null)
            {
                throw new OffersServiceConnectionException();
            }
            if(!response.IsSuccessStatusCode)
            {
                throw new OffersServiceException(response.ReasonPhrase);
            }

            offer.Confirm();
            await _offerSnippetRepository.UpdateAsync(offer);
        }

        
    }
}
