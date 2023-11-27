using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.DTO
{
    public class VolumeDto
    {
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
    }
}
