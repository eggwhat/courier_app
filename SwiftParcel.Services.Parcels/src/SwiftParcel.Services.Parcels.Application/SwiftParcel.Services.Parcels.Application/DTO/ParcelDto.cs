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
        public Guid Id { get; protected set; }
        public Guid OrderId { get; protected set; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public decimal Price { get; protected set; }
        public IEnumerable<string> Source { get; protected set; }
        public IEnumerable<string> Destination { get; protected set; }
        public string Variant { get; protected set; }
        public string Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public bool IsFragile { get; protected set; }
    }
}
