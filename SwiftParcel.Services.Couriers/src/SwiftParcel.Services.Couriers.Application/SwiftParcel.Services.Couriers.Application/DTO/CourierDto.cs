using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Couriers.Application.DTO
{
    public class CourierDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // public string Brand { get; set; }
        // public string Model { get; set; }
        public string Description { get; set; }
        public double PayloadCapacity { get; set; }
        public double LoadingCapacity { get; set; }
        public decimal PricePerService { get; set; }
        public IEnumerable<string> Variants { get; set; }
    }
}