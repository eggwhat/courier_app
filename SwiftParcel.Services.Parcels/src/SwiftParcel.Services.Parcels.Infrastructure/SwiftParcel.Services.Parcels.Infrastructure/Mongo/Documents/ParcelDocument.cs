using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Convey.Types;
using SwiftParcel.Services.Parcels.Core.Entities;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents
{
    internal class ParcelDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }  
        public string Description { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Priority Priority { get; set; }
        public bool AtWeekend { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsCompany { get; set; }
        public bool VipPackage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal CalculatedPrice { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }
    }
}