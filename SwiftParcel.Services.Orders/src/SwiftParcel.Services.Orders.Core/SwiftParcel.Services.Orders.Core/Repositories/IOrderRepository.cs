namespace SwiftParcel.Services.Orders.Core.Entities
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(Guid id);
        Task<Order> GetAsync(Guid currierId, DateTime deliveryDate);
        Task<Order> GetContainingParcelAsync(Guid parcelId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}


