using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Core.Entities;

namespace SwiftParcel.Services.Customers.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(Guid id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}