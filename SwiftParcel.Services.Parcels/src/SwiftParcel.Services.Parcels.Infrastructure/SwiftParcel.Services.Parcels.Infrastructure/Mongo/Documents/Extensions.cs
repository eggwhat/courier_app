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
                document.Description,
                document.Width,
                document.Height,
                document.Depth,
                document.Weight,
                new Address(document.Source.Street,
                            document.Source.BuildingNumber,
                            document.Source.ApartmentNumber,
                            document.Source.City,
                            document.Source.ZipCode,
                            document.Source.Country
                            ),
                new Address(document.Destination.Street,
                            document.Destination.BuildingNumber,
                            document.Destination.ApartmentNumber,
                            document.Destination.City,
                            document.Destination.ZipCode,
                            document.Destination.Country),
                document.Priority,
                document.AtWeekend,
                document.PickupDate,
                document.DeliveryDate,
                document.IsCompany,
                document.VipPackage,
                document.CreatedAt,
                document.CalculatedPrice,
                document.PriceBreakDown,
                document.ValidTo,
                document.CustomerId
                );

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
                Source = new Address()
                {
                    Street = entity.Source.Street,
                    BuildingNumber = entity.Source.BuildingNumber,
                    ApartmentNumber = entity.Source.ApartmentNumber,
                    City = entity.Source.City,
                    ZipCode = entity.Source.ZipCode,
                    Country = entity.Source.Country
                },
                Destination = new Address()
                {
                    Street = entity.Destination.Street,
                    BuildingNumber = entity.Destination.BuildingNumber,
                    ApartmentNumber = entity.Destination.ApartmentNumber,
                    City = entity.Destination.City,
                    ZipCode = entity.Destination.ZipCode,
                    Country = entity.Destination.Country
                },
                Priority = entity.Priority,
                AtWeekend = entity.AtWeekend,
                PickupDate = entity.PickupDate,
                DeliveryDate = entity.DeliveryDate,
                IsCompany = entity.IsCompany,
                VipPackage = entity.VipPackage,
                CreatedAt = entity.CreatedAt,
                ValidTo = entity.ValidTo,
                CalculatedPrice = entity.CalculatedPrice,
                PriceBreakDown = entity.PriceBreakDown
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
                Source = new AddressDto
                {
                    Street = document.Source.Street,
                    BuildingNumber = document.Source.BuildingNumber,
                    ApartmentNumber = document.Source.ApartmentNumber,
                    City = document.Source.City,
                    ZipCode = document.Source.ZipCode,
                    Country = document.Source.Country
                },
                Destination = new AddressDto
                {
                    Street = document.Destination.Street,
                    BuildingNumber = document.Destination.BuildingNumber,
                    ApartmentNumber = document.Destination.ApartmentNumber,
                    City = document.Destination.City,
                    ZipCode = document.Destination.ZipCode,
                    Country = document.Destination.Country
                },
                Priority = document.Priority.ToString(),
                AtWeekend = document.AtWeekend,
                PickupDate = document.PickupDate,
                DeliveryDate = document.DeliveryDate,
                IsCompany = document.IsCompany,
                VipPackage = document.VipPackage,
                CreatedAt = document.CreatedAt,
                ValidTo = document.ValidTo,
                CalculatedPrice = document.CalculatedPrice,
                PriceBreakDown = document.PriceBreakDown.AsDto()
            };

        public static Customer AsEntity(this CustomerDocument document)
            => new Customer(document.Id);

        public static CustomerDocument AsDocument(this Customer entity)
            => new CustomerDocument
            {
                Id = entity.Id
            };
        
        public static List<PriceBreakDownItemDto> AsDto(this List<PriceBreakDownItem> priceBreakDown)
        {
            var priceBreakDownDto = new List<PriceBreakDownItemDto>();
            foreach (var item in priceBreakDown)
            {
                priceBreakDownDto.Add(new PriceBreakDownItemDto
                {
                    Amount = item.Amount,
                    Currency = item.Currency,
                    Description = item.Description
                });
            }
            return priceBreakDownDto;
        }
    }
}
