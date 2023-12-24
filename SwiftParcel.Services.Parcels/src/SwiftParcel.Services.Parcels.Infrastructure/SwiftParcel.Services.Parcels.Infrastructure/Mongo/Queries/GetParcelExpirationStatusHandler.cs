using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Queries;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;
using SwiftParcel.Services.Parcels.Application;



namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetParcelExpirationStatusHandler : IQueryHandler<GetParcelExpirationStatus, ExpirationStatusDto>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;
        public GetParcelExpirationStatusHandler(IMongoRepository<ParcelDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ExpirationStatusDto> HandleAsync(GetParcelExpirationStatus query, CancellationToken cancellationToken)
        {
            var document = await _repository.GetAsync(v => v.Id == query.ParcelId);
            return new ExpirationStatusDto
            {
                ParcelId = document.Id,
                TotalPrice = document.CalculatedPrice,
                ExpiringAt = document.ValidTo
            };
        }
    }
}