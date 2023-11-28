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
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Variant Variant { get; set; }
        public Priority Priority { get; set; }
        public bool AtWeekend { get; set; }
        public bool IsFragile { get; set; }

        public class Address
        {
            public string Street { get; set; }
            public string BuildingNumber { get; set; }
            public string ApartmentNumber { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
        }
    }
}