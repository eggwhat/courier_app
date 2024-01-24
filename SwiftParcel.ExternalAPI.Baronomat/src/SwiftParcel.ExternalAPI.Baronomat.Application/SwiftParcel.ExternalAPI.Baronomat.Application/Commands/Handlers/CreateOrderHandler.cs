using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions;
using SwiftParcel.ExternalAPI.Baronomat.Core.Repositories;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrdersServiceClient _ordersServiceClient;
        private readonly IOfferSnippetRepository _offerSnippetRepository;

        public CreateOrderHandler(IOrdersServiceClient ordersServiceClient, IOfferSnippetRepository offerSnippetRepository)
        {
            _ordersServiceClient = ordersServiceClient;
            _offerSnippetRepository = offerSnippetRepository;
        }
        public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken)
        {
            var orderRequest = new OrderRequestDto(command);
            var response = await _ordersServiceClient.PostAsync(orderRequest);
            if(response == null)
            {
                throw new OffersServiceConnectionException();
            }
            if (!response.Response.IsSuccessStatusCode)
            {
                throw new OffersServiceException(response.Response.ReasonPhrase);
            }   

            var offerSnippet = new OrderSnippet(command.CustomerId, response.Result.Id);

            await _offerSnippetRepository.AddAsync(offerSnippet);
        }
    }
}