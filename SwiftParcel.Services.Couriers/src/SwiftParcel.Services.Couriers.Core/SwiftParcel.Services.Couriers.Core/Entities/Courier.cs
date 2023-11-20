using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Couriers.Core.Exceptions;

namespace SwiftParcel.Services.Couriers.Core.Entities
{
    public class Courier
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }
        
        // public string Brand { get; protected set; }
        // public string Model { get; protected set; }
        public string Description { get; protected set; }
        public double PayloadCapacity { get; protected set; }
        public double LoadingCapacity { get; protected set; }
        public decimal PricePerService { get; protected set; }
        public Variants Variants { get; protected set; }

        // public Courier(Guid id, string brand, string model, string description, double payloadCapacity,
        //     double loadingCapacity, decimal pricePerService)
         public Courier(Guid id, string name, string description, double payloadCapacity,
            double loadingCapacity, decimal pricePerService)
        {
            Id = id;
            Name = name;
            ChangeDescription(description);
            PayloadCapacity = payloadCapacity > 0 ? payloadCapacity : throw new InvalidCourierCapacity(payloadCapacity);
            LoadingCapacity = loadingCapacity > 0 ? loadingCapacity : throw new InvalidCourierCapacity(loadingCapacity);
            ChangePricePerService(pricePerService);
            AddVariants(Variants.Standard);
        }
        
        // public Courier(Guid id, string brand, string model, string description, double payloadCapacity,
        //     double loadingCapacity, decimal pricePerService, params Variants[] variants) 
        //       : this(id, brand, model, description, payloadCapacity, loadingCapacity, pricePerService)
        public Courier(Guid id, string name, string description, double payloadCapacity,
            double loadingCapacity, decimal pricePerService, params Variants[] variants) 
            : this(id, name, description, payloadCapacity, loadingCapacity, pricePerService)
        {
            AddVariants(variants);
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidCourierDescriptionException(description);
            }

            Description = description;
        }

        public void ChangePricePerService(decimal pricePerService)
        {
            if (pricePerService <= 0)
            {
                throw  new InvalidCourierPricePerServiceException(pricePerService);
            }

            PricePerService = pricePerService;
        }

        public void ChangeVariants(Variants variants)
            => Variants = variants;

        public void AddVariants(params Variants[] variants)
        {
            foreach (var variant in variants)
            {
                Variants |= variant;
            }
        }
        
        public void RemoveVariants(params Variants[] variants)
        {
            foreach (var variant in variants)
            {
                Variants &= ~variant;
            }
        }
    }
}
