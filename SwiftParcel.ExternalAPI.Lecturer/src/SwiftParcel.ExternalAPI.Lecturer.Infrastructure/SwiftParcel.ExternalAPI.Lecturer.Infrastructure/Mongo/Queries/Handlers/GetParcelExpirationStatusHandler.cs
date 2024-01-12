using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.ExternalAPI.Lecturer.Application.DTO;
using SwiftParcel.ExternalAPI.Lecturer.Application.Queries;
using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;
using SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Documents;

namespace SwiftParcel.ExternalAPI.Lecturer.Infrastructure.Mongo.Queries.Handlers
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
                ParcelId = document.InquiryId,
                TotalPrice = (decimal)document.TotalPrice,
                ExpiringAt = document.ExpiringAt,
                PriceBreakDown = document.PriceBreakDown.AsDto(),
                CompanyName = Company.MiniCurrier.ToString()
            };
            return dto;
        }
    }
}