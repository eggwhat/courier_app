using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwiftParcel.Services.Parcels.Core.Exceptions;

namespace SwiftParcel.Services.Parcels.Core.Entities
{
    public class Parcel
    {
        public Guid Id { get; protected set; }
        public string Description { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Weight { get; protected set; }
        public DateTime PickedUpAt { get; protected set; }
        public DateTime DeliveryAt { get; protected set; }
        public Address Source { get; protected set; }
        public Address Destination { get; protected set; }
        public Variants Variant { get; protected set; }
        public Priority Priority { get; protected set; }
        public bool AtWeekend { get; protected set; }
        public bool IsFragile { get; protected set; }

        public Parcel(Guid id, string description, double width, double height, double depth,
            double weight, DateTime pickedUpAt, DateTime deliveryAt, Address source, Address destination,
            Variants variant, Priority priority, bool atWeekend, bool isFragile)
        {
            Id = id;
            CheckDescription(description);
            Description = description;
            CheckDimensions(width, height, depth);
            Width = width;
            Height = height;
            Depth = depth;
            CheckWeight(weight);
            Weight = weight;
            PickedUpAt = pickedUpAt;
            DeliveryAt = deliveryAt;
            Source = source;
            Destination = destination;
            Variant = variant;
            Priority = priority;
            AtWeekend = atWeekend;
            IsFragile = isFragile;
        }

        public void CheckDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidParcelDescriptionException(description);
            }
            
            Description = description;
        }

        public void CheckDimensions(double width, double height, double depth)
        {
            if (width <= 0)
            {
                throw new InvalidParcelDimensionException("width", width);
            }

            if (height <= 0)
            {
                throw new InvalidParcelDimensionException("height", height);
            }

            if (depth <= 0)
            {
                throw new InvalidParcelDimensionException("depth", depth);
            }

            Width = width;
            Height = height;
            Depth = depth;
        }

        public void CheckWeight(double weight)
        {
            if (weight <= 0)
            {
                throw new InvalidParcelWeightException(weight);
            }

            Weight = weight;
        }
    }
}
