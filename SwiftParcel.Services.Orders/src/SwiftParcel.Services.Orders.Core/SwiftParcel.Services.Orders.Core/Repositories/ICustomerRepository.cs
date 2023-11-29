using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(Customer customer);
    }
}

