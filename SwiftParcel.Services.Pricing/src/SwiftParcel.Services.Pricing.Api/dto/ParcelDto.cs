using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.dto
{
    public class ParcelDto
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool HighPriority { get; set; }
        public bool DeliverAtWeekend { get; set; }
        public bool VipPackage { get; set; }
    }
}