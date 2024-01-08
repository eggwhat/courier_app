using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Lecturer.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Lecturer.Core.Repositories;
using SwiftParcel.ExternalAPI.Lecturer.Application.Exceptions;

namespace SwiftParcel.ExternalAPI.Lecturer.Application.Commands.Handlers
{
    public class CancelOrderHandler : ICommandHandler<CancelOrder>
    {
        private readonly IIdentityManagerServiceClient _identityManagerServiceClient;
        private readonly IOfferSnippetRepository _offerSnippetRepository;
        private readonly IOffersServiceClient _officeServiceClient;

        public CancelOrderHandler(IIdentityManagerServiceClient identityManagerServiceClient, 
            IOfferSnippetRepository offerSnippetRepository, IOffersServiceClient officeServiceClient)
        {
            _identityManagerServiceClient = identityManagerServiceClient;
            _offerSnippetRepository = offerSnippetRepository;
            _officeServiceClient = officeServiceClient;
        }

        public async Task HandleAsync(CancelOrder command, CancellationToken cancellationToken)
        {
            var offer = await _offerSnippetRepository.GetAsync(command.OrderId);
            if (offer is null)
            {
                throw new OfferNotFoundException(command.OrderId);
            }

            var token = await _identityManagerServiceClient.GetToken();
            var response = await _officeServiceClient.DeleteCancelOffer(token, command.OrderId.ToString());
            if(response is null)
            {
                throw new OffersServiceConnectionException();
            }
            if(!response.Response.IsSuccessStatusCode)
            {
                throw new OffersServiceException(response.Response.ReasonPhrase);
            }

            offer.Cancel();
            await _offerSnippetRepository.UpdateAsync(offer);
        }
    }
}