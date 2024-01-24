using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.ExternalAPI.Baronomat.Application.DTO;
using SwiftParcel.ExternalAPI.Baronomat.Application.Queries;
using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;
using SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Documents;

namespace SwiftParcel.ExternalAPI.Baronomat.Infrastructure.Mongo.Queries.Handlers
{
    public class GetParcelExpirationStatusHandler : IQueryHandler<GetParcelExpirationStatus, ExpirationStatusDto>
    {
        public IMongoRepository<InquiryOfferDocument, Guid> _repository { get; set; }
        public GetParcelExpirationStatusHandler(IMongoRepository<InquiryOfferDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ExpirationStatusDto> HandleAsync(GetParcelExpirationStatus query, CancellationToken cancellationToken)
        {
            var document = await _repository.GetAsync(v => v.Id == query.ParcelId);
            if(document is null)
            {
                return null;
            }
            var dto = new ExpirationStatusDto()
            {
                ParcelId = document.Id,
                TotalPrice = (decimal)document.TotalPrice,
                ExpiringAt = document.ExpiringAt,
                PriceBreakDown = document.PriceBreakDown.AsDto(),
                CompanyName = Company.Baronomat.ToString()
            };
            return dto;
        }
    }
}