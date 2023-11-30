using SwiftParcel.Services.Parcels.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Services
{
    public class ParcelService : IParcelService
    {
        public double CalculateVolume(IEnumerable<Parcel> parcels)
        {
            double volume = 0.0;

            foreach (var parcel in parcels)
            {
                volume += CalculateVolumeOfParcel(parcel);
            }

            return volume;
        }

        private static double CalculateVolumeOfParcel(Parcel parcel)
            => parcel.Width * parcel.Height * parcel.Depth;
    }
}
