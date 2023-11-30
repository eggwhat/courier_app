using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpCompress.Common;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Core.Entities;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static Parcel AsEntity(this ParcelDocument document)
            => document is null? null : new Parcel(
                document.Id,
                document.CustomerId,
                document.Name,
                document.Description,
                document.Width,
                document.Height,
                document.Depth,
                document.Weight,
                document.Price,
                new Address(document.Source.Street,
                            document.Source.BuildingNumber,
                            document.Source.ApartmentNumber,
                            document.Source.City,
                            document.Source.ZipCode),
                new Address(document.Destination.Street,
                            document.Destination.BuildingNumber,
                            document.Destination.ApartmentNumber,
                            document.Destination.City,
                            document.Destination.ZipCode),
                document.Variant,
                document.Priority,
                document.AtWeekend,
                document.IsFragile,
                document.CreatedAt,
                document.OrderId);

        public static async Task<Parcel> AsEntityAsync(this Task<ParcelDocument> task)
            => (await task).AsEntity();

        public static ParcelDocument AsDocument(this Parcel entity)
            => new ParcelDocument
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                Description = entity.Description,
                Width = entity.Width,
                Height = entity.Height,
                Depth = entity.Depth,
                Weight = entity.Weight,
                Price = entity.Price,
                Source = new ParcelDocument.Address
                {
                    Street = entity.Source.Street,
                    BuildingNumber = entity.Source.BuildingNumber,
                    ApartmentNumber = entity.Source.ApartmentNumber,
                    City = entity.Source.City,
                    ZipCode = entity.Source.ZipCode,
                },
                Destination = new ParcelDocument.Address
                {
                    Street = entity.Destination.Street,
                    BuildingNumber = entity.Destination.BuildingNumber,
                    ApartmentNumber = entity.Destination.ApartmentNumber,
                    City = entity.Destination.City,
                    ZipCode = entity.Destination.ZipCode,
                },
                Variant = entity.Variant,
                Priority = entity.Priority,
                AtWeekend = entity.AtWeekend,
                IsFragile = entity.IsFragile,
                CreatedAt = entity.CreatedAt,
                OrderId = entity.OrderId
            };
        
        public static async Task<ParcelDocument> AsDocumentAsync(this Task<Parcel> task)
            => (await task).AsDocument();

        public static ParcelDto AsDto(this ParcelDocument document)
            => new ParcelDto
            {
                Id = document.Id,
                CustomerId = document.CustomerId,
                Description = document.Description,
                Width = document.Width,
                Height = document.Height,
                Depth = document.Depth,
                Weight = document.Weight,
                Price = document.Price,
                Source = new AddressDto
                {
                    Street = document.Source.Street,
                    BuildingNumber = document.Source.BuildingNumber,
                    ApartmentNumber = document.Source.ApartmentNumber,
                    City = document.Source.City,
                    ZipCode = document.Source.ZipCode,
                },
                Destination = new AddressDto
                {
                    Street = document.Destination.Street,
                    BuildingNumber = document.Destination.BuildingNumber,
                    ApartmentNumber = document.Destination.ApartmentNumber,
                    City = document.Destination.City,
                    ZipCode = document.Destination.ZipCode,
                },
                Variant = document.Variant.ToString().ToLowerInvariant(),
                Priority = document.Priority.ToString().ToLowerInvariant(),
                AtWeekend = document.AtWeekend,
                IsFragile = document.IsFragile,
                CreatedAt = document.CreatedAt,
                OrderId = document.OrderId
            };

        public static Customer AsEntity(this CustomerDocument document)
            => new Customer(document.Id);

        public static CustomerDocument AsDocument(this Customer entity)
            => new CustomerDocument
            {
                Id = entity.Id
            };
    }
}
