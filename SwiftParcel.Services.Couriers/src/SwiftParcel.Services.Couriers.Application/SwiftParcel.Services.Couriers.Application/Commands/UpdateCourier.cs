using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Couriers.Core.Entities;

namespace SwiftParcel.Services.Couriers.Application.Commands
{
    public class UpdateCourier : ICommand
    {
        public Guid CourierId { get; }
        public string Description { get; }
        public decimal PricePerService { get; }
        public Variants Variants { get; }

        public UpdateCourier(Guid courierId,string description, decimal pricePerService, Variants variants)
        {
            CourierId = courierId;
            Description = description;
            PricePerService = pricePerService;
            Variants = variants;
        }
    }
}