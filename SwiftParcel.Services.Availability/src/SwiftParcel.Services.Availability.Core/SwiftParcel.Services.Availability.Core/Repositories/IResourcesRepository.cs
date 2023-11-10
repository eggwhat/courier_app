using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Availability.Core.Repositories
{
    public interface IResourcesRepository
    {
        Task<Resource> GetAsync(AggregateId id);
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(Resource resource);
        Task UpdateAsync(Resource resource);
        Task DeleteAsync(AggregateId id);
    }
}