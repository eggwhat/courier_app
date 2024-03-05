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
        private readonly IInquiryOfferRepository _inquiryOfferRepository;

        public CreateOrderHandler(IOrdersServiceClient ordersServiceClient, IOfferSnippetRepository offerSnippetRepository,
            IInquiryOfferRepository inquiryOfferRepository)
        {
            _ordersServiceClient = ordersServiceClient;
            _offerSnippetRepository = offerSnippetRepository;
            _inquiryOfferRepository = inquiryOfferRepository;
        }
        public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken)
        {
            var inquiryOffer = await _inquiryOfferRepository.GetAsync(command.ParcelId);
            if (inquiryOffer is null)
            {
                throw new InquiryOfferNotFoundException(command.ParcelId);
            }
            var orderRequest = new OrderRequestDto(command, inquiryOffer.TotalPrice);
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