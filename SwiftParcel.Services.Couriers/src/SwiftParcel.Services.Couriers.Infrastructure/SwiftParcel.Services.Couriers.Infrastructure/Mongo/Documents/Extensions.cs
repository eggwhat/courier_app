using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Couriers.Application.DTO;
using SwiftParcel.Services.Couriers.Core.Entities;

namespace SwiftParcel.Services.Couriers.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static Courier AsEntity(this CourierDocument document)
            => document is null? null : new Courier(
                document.Id,
                // document.Brand,
                // document.Model,
                document.Name,
                document.LastName,
                document.Description,
                document.PayloadCapacity,
                document.LoadingCapacity,
                document.PricePerService,
                document.Variants);

        public static async Task<Courier> AsEntityAsync(this Task<CourierDocument> task)
            => (await task).AsEntity();

        public static CourierDocument AsDocument(this Courier entity)
            => new CourierDocument
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Description = entity.Description,
                PayloadCapacity = entity.PayloadCapacity,
                LoadingCapacity = entity.LoadingCapacity,
                PricePerService = entity.PricePerService,
                Variants = entity.Variants
            };
        
        public static async Task<CourierDocument> AsDocumentAsync(this Task<Courier> task)
            => (await task).AsDocument();

        public static CourierDto AsDto(this CourierDocument document)
            => new CourierDto
            {
                Id = document.Id,
                // Brand = document.Brand,
                // Model = document.Model,
                Name = document.Name;
                
                Description = document.Description,
                PayloadCapacity = document.PayloadCapacity,
                LoadingCapacity = document.LoadingCapacity,
                PricePerService = document.PricePerService,
                Variants = document.Variants.ToString().Split(',')
            };
    }
}