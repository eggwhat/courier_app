using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Queries;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;
using SwiftParcel.Services.Parcels.Application;



namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetParcelValidationStatusHandler : IQueryHandler<GetParcelValidationStatus, ValidationStatusDto>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;
        public GetParcelValidationStatusHandler(IMongoRepository<ParcelDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ValidationStatusDto> HandleAsync(GetParcelValidationStatus query, CancellationToken cancellationToken)
        {
            var document = await _repository.GetAsync(v => v.Id == query.ParcelId);
            return new ValidationStatusDto
            {
                ParcelId = document.Id,
                TotalPrice = document.CalculatedPrice,
                ExpiringAt = document.ValidTo
            };
        }
    }
}