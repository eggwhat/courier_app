using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwiftParcel.Services.Parcels.Core.Entities;

namespace SwiftParcel.Services.Parcels.Core.Services
{
    public interface IParcelService
    {
        double CalculateVolume(IEnumerable<Parcel> parcels);
    }
}
