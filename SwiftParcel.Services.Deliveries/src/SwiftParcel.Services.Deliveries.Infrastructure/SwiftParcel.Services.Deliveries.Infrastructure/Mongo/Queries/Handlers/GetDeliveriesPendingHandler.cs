using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Deliveries.Application.DTO;
using SwiftParcel.Services.Deliveries.Application.Queries;
using SwiftParcel.Services.Deliveries.Core.Entities;
using SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Documents;


namespace SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Queries.Handlers
{
    public class GetDeliveriesPendingHandler: IQueryHandler<GetDeliveriesPending, IEnumerable<DeliveryDto>>
    {
        private readonly IMongoRepository<DeliveryDocument, Guid> _repository;

        public GetDeliveriesPendingHandler(IMongoRepository<DeliveryDocument, Guid> repository)
            => _repository = repository;

        public async Task<IEnumerable<DeliveryDto>> HandleAsync(GetDeliveriesPending query)
        {
            var documents = await _repository.FindAsync(d => d.Status == DeliveryStatus.Unassigned);

            return documents.Select(d => d.AsDto());
        }
    }
}