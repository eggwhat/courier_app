using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Deliveries.Application.DTO;
using SwiftParcel.Services.Deliveries.Application.Queries;
using SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Queries.Handlers
{
    public class GetDeliveriesHandler: IQueryHandler<GetDeliveries, IEnumerable<DeliveryDto>>
    {
        private readonly IMongoRepository<DeliveryDocument, Guid> _repository;

        public GetDeliveriesHandler(IMongoRepository<DeliveryDocument, Guid> repository)
            => _repository = repository;

        public async Task<IEnumerable<DeliveryDto>> HandleAsync(GetDeliveries query)
        {
            var documents = await _repository.FindAsync(d => d.CourierId == query.CourierId);

            return documents.Select(d => d.AsDto());
        }
    }
}