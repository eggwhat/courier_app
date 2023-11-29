using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Couriers.Core.Entities;

namespace SwiftParcel.Services.Couriers.Application.Commands
{
    public class AddCourier : ICommand
    {
        public Guid CourierId { get; }

        public string Name { get; }
        // public string Brand { get; }
        // public string Model { get; }
        public string Description { get; }
        public double PayloadCapacity { get; }
        public double LoadingCapacity { get; }
        public decimal PricePerService { get; }
        public Variants Variants { get; }
        
        // public AddVehicle(Guid vehicleId, string brand, string model, string description, double payloadCapacity, 
        //     double loadingCapacity, decimal pricePerService, Variants variants)
        public AddCourier(Guid courierId, string name, string description, double payloadCapacity, 
            double loadingCapacity, decimal pricePerService, Variants variants)
        {
            CourierId = courierId == Guid.Empty ? Guid.NewGuid() : courierId;
            Name = name;
            Description = description;
            PayloadCapacity = payloadCapacity;
            LoadingCapacity = loadingCapacity;
            PricePerService = pricePerService;
            Variants = variants;
        }
        
    }
}