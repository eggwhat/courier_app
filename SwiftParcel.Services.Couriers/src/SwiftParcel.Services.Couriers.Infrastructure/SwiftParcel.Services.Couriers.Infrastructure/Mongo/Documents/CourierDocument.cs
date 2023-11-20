using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Couriers.Core.Entities;

namespace SwiftParcel.Services.Couriers.Infrastructure.Mongo.Documents
{
    internal class CourierDocument
    {
        public Guid Id { get; set; }
        // public string Brand { get; set; }
        // public string Model { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public double PayloadCapacity { get; set; }
        public double LoadingCapacity { get; set; }
        public decimal PricePerService { get; set; }
        public Variants Variants { get; set; }
    }
}