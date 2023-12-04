using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Entities
{
    public enum Variant
    {
        Standard = 1 << 0,
        Chemistry = 1 << 1,
        Weapon = 1 << 2,
        Animal = 1 << 3,
        Organ = 1 << 4
    }
}
