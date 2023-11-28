using Convey.CQRS.Commands;
using SwiftParcel.Services.Parcels.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Commands
{
    public class AddParcel : ICommand
    {
        public Guid ParcelId { get; protected set; }
        public Guid OrderId { get; protected set; }
        public Guid CustomerId { get; protected set; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public decimal Price { get; protected set; }
        public Address Source { get; protected set; }
        public Address Destination { get; protected set; }
        public Variant Variant { get; protected set; }
        public Priority Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public bool IsFragile { get; protected set; }

        public AddParcel(Guid parcelId, Guid orderId, Guid customerId, string description,
            double width, double height, double depth, double weight)
        {
            ParcelId = parcelId == Guid.Empty ? Guid.NewGuid() : parcelId;
            OrderId = orderId;
            CustomerId = customerId;
            Description = description;
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            Source = new Address();
            Destination = new Address();
        }
    }
}
