using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwiftParcel.Services.Parcels.Core.Entities;

namespace SwiftParcel.Services.Parcels.Core.Repositories
{
    public interface IParcelRepository
    {
        Task<Parcel> GetAsync(Guid id);
        Task AddAsync(Parcel courier);
        Task DeleteAsync(Parcel courier);
    }
}
