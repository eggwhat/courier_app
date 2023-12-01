using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Deliveries.Core.Entities;
using SwiftParcel.Services.Deliveries.Core.Repositories;
using SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Repositories
{
    internal class DeliveriesMongoRepository : IDeliveriesRepository
    {
        private readonly IMongoRepository<DeliveryDocument, Guid> _repository;

        public DeliveriesMongoRepository(IMongoRepository<DeliveryDocument, Guid> repository)
            => _repository = repository;

        public async Task<Delivery> GetAsync(Guid id)
        {
            var document = await _repository.GetAsync(d => d.Id == id);
            return document?.AsEntity();
        }

        public async Task<Delivery> GetForOrderAsync(Guid id)
        {
            var document = await _repository.GetAsync(d => d.OrderId == id);
            return document?.AsEntity();
        }

        public Task AddAsync(Delivery delivery)
            => _repository.AddAsync(delivery.AsDocument());
        
        public Task UpdateAsync(Delivery delivery)
            => _repository.UpdateAsync(delivery.AsDocument());

        public Task DeleteAsync(Delivery delivery)
            => _repository.DeleteAsync(delivery.Id);
    }
}