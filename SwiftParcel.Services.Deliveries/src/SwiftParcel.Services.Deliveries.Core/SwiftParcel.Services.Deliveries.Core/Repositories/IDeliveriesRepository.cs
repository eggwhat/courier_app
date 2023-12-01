using SwiftParcel.Services.Deliveries.Core.Entities;

namespace SwiftParcel.Services.Deliveries.Core.Repositories
{
    public interface IDeliveriesRepository
    {
        Task<Delivery> GetAsync(Guid id);
        Task<Delivery> GetForOrderAsync(Guid number);
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task DeleteAsync(Delivery delivery);
    }
}