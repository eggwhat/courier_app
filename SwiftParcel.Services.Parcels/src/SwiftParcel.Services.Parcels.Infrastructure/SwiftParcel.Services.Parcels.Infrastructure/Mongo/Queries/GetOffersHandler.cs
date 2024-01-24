using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Queries;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;
using SwiftParcel.Services.Parcels.Application;
using SwiftParcel.Services.Parcels.Application.Services.Clients;



namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetOffersHandler : IQueryHandler<GetOffers, IEnumerable<ExpirationStatusDto>>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;
        private readonly ILecturerApiServiceClient _lecturerApiServiceClient;
        private readonly IBaronomatApiServiceClient _baronomatApiServiceClient;
        private readonly string _companyName = "SwiftParcel";
        public GetOffersHandler(IMongoRepository<ParcelDocument, Guid> repository,
            ILecturerApiServiceClient lecturerApiServiceClient, IBaronomatApiServiceClient baronomatApiServiceClient)
        {
            _repository = repository;
            _lecturerApiServiceClient = lecturerApiServiceClient;
            _baronomatApiServiceClient = baronomatApiServiceClient;
        }

        public async Task<IEnumerable<ExpirationStatusDto>> HandleAsync(GetOffers query, CancellationToken cancellationToken)
        {
            var document = await _repository.GetAsync(v => v.Id == query.ParcelId);
            var offer = new ExpirationStatusDto
            {
                ParcelId = document.Id,
                TotalPrice = document.CalculatedPrice,
                ExpiringAt = document.ValidTo,
                PriceBreakDown = document.PriceBreakDown.AsDto(),  
                CompanyName = _companyName
            };
            var offerLecturerApi = await _lecturerApiServiceClient.GetOfferAsync(query.ParcelId);
            var offerBaronomatApi = await _baronomatApiServiceClient.GetOfferAsync(query.ParcelId);
            return new List<ExpirationStatusDto> { offer, offerLecturerApi, offerBaronomatApi };
        }
    }
}