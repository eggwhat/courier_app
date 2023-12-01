using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Deliveries.Application.DTO;
using SwiftParcel.Services.Deliveries.Application.Queries;
using SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Queries.Handlers
{
    public class GetDeliveryHandler : IQueryHandler<GetDelivery, DeliveryDto>
    {
        private readonly IMongoRepository<DeliveryDocument, Guid> _repository;

        public GetDeliveryHandler(IMongoRepository<DeliveryDocument, Guid> repository)
            => _repository = repository;

        public async Task<DeliveryDto> HandleAsync(GetDelivery query)
        {
            var document = await _repository.GetAsync(d => d.Id == query.DeliveryId);

            return document?.AsDto();
        }
    }
}