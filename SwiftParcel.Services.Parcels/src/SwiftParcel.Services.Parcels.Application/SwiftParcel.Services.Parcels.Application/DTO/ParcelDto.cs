using SwiftParcel.Services.Parcels.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.DTO
{
    public class ParcelDto
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
        public AddressDto Source { get; set; }
        public AddressDto Destination { get; set; }
        public string Variant { get; set; }
        public string Priority { get; set; }
        public bool AtWeekend { get; set; }
        public bool IsFragile { get; set; }
    }
}
