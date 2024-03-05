using Convey.CQRS.Commands;
using SwiftParcel.ExternalAPI.Baronomat.Application.Services.Clients;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Application.Exceptions;
using SwiftParcel.ExternalAPI.Baronomat.Core.Repositories;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;
using SwiftParcel.ExternalAPI.Baronomat.Application.Services;

namespace SwiftParcel.ExternalAPI.Baronomat.Application.Commands.Handlers
{
    public class AddParcelHandler: ICommandHandler<AddParcel>
    {
        private readonly IPriceCalculatorClient _priceCalculatorClient;
        private readonly IInquiryOfferRepository _inquiryOfferRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly int _milimetrToMeterScale = 1000;
        private readonly int _gramToKilogramScale = 1000;
        private readonly string _currency = "Pln";
        private readonly string _description = "Full price";
        public AddParcelHandler(IPriceCalculatorClient priceCalculatorClient, IInquiryOfferRepository inquiryOfferRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _priceCalculatorClient = priceCalculatorClient;
            _inquiryOfferRepository = inquiryOfferRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task HandleAsync(AddParcel command, CancellationToken cancellationToken)
        {
            var deliveryDate = DateTime.Parse(command.DeliveryDate);
            var PriceRequestDto = new PriceRequestDto(command.Width * _milimetrToMeterScale, command.Height * _milimetrToMeterScale, 
                command.Depth * _milimetrToMeterScale, command.Weight * _gramToKilogramScale, deliveryDate, command.Priority, command.AtWeekend);
           
            var response = await _priceCalculatorClient.PostAsync(PriceRequestDto);
            if(response == null)
            {
                throw new PriceCalculatorServiceConnectionException();
            }
            if (!response.Response.IsSuccessStatusCode)
            {
                throw new PriceCalculatorServiceException(response.Response.ReasonPhrase);
            }
            var priceBreakDown = new List<PriceBreakDownItem>();
            double price = (double)response.Result.PriceCents / 100;
            priceBreakDown.Add(new PriceBreakDownItem(price, _currency, _description));

            var inquiryOffer = new InquiryOffer(command.ParcelId, price, _dateTimeProvider.Now.AddMinutes(60), priceBreakDown);
            await _inquiryOfferRepository.AddAsync(inquiryOffer);
        }
    }
}
