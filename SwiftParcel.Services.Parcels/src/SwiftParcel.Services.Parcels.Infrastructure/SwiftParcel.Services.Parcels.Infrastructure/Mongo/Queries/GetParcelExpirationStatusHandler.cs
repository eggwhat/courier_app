using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Queries;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;
using SwiftParcel.Services.Parcels.Application;
using SwiftParcel.Services.Parcels.Application.Services.Clients;



namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetParcelExpirationStatusHandler : IQueryHandler<GetParcelExpirationStatus, IEnumerable<ExpirationStatusDto>>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;
        private readonly ILecturerApiServiceClient _lecturerApiServiceClient;
        private readonly string _companyName = "SwiftParcel";
        public GetParcelExpirationStatusHandler(IMongoRepository<ParcelDocument, Guid> repository,
            ILecturerApiServiceClient lecturerApiServiceClient)
        {
            _repository = repository;
            _lecturerApiServiceClient = lecturerApiServiceClient;
        }

        public async Task<IEnumerable<ExpirationStatusDto>> HandleAsync(GetParcelExpirationStatus query, CancellationToken cancellationToken)
        {
            var document = await _repository.GetAsync(v => v.Id == query.ParcelId);
            var offer = new ExpirationStatusDto
            {
                ParcelId = document.Id,
                TotalPrice = document.CalculatedPrice,
                ExpiringAt = document.ValidTo,
                PriceBreakDown = null,
                CompanyName = _companyName
            };
            var offerLecturerApi = await _lecturerApiServiceClient.GetOfferAsync(query.ParcelId);
            return new List<ExpirationStatusDto> { offer, offerLecturerApi };
        }
    }
}