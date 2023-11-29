using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Couriers.Core.Entities;

namespace SwiftParcel.Services.Couriers.Core.Repositories
{
    public interface ICourierRepository
    {
        Task<Courier> GetAsync(Guid id);
        Task AddAsync(Courier courier);
        Task UpdateAsync(Courier courier);
        Task DeleteAsync(Courier courier);
    }
}