using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Pricing.Api.Core.Entities
{
    public class Parcel
    {
        public double Length { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public double Weight { get; private set; }
        public bool HighPriority { get; private set; }
        public bool DeliverAtWeekend { get; private set; }
        public bool VipPackage { get; private set; }

        public Parcel(double length, double width, double height, double weight, bool highPriority, 
            bool deliverAtWeekend, bool vipPackage)
        {
            Length = length;
            Width = width;
            Height = height;
            Weight = weight;
            HighPriority = highPriority;
            DeliverAtWeekend = deliverAtWeekend;
            VipPackage = vipPackage;
        }
    }
}