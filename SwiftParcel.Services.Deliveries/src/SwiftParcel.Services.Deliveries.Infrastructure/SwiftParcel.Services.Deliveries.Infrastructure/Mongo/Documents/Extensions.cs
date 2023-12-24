using SwiftParcel.Services.Deliveries.Application.DTO;
using SwiftParcel.Services.Deliveries.Core.Entities;

namespace SwiftParcel.Services.Deliveries.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static DeliveryDocument AsDocument(this Delivery delivery)
            => new DeliveryDocument
            {
                Id = delivery.Id,
                OrderId = delivery.OrderId,
                CourierId = delivery.CourierId,
                Status = delivery.Status,
                Volume = delivery.Volume,
                Weight = delivery.Weight,
                Source = delivery.Source,
                Destination = delivery.Destination,
                Priority = delivery.Priority,
                AtWeekend = delivery.AtWeekend,
                PickupDate = delivery.PickupDate,
                DeliveryDate = delivery.DeliveryDate,
                DeliveryAttemptDate = delivery.DeliveryAttemptDate,
                CannotDeliverReason = delivery.CannotDeliverReason,
                LastUpdate = delivery.LastUpdate
            };
 
        public static Delivery AsEntity(this DeliveryDocument document)
            => new Delivery(document.Id, document.OrderId, document.CourierId,
             document.LastUpdate, document.Status, document.Volume, document.Weight,
             document.Source, document.Destination, document.Priority, document.AtWeekend,
             document.PickupDate, document.DeliveryDate, document.DeliveryAttemptDate,
             document.CannotDeliverReason);

        public static DeliveryDto AsDto(this DeliveryDocument document)
            => new DeliveryDto
            {
                Id = document.Id,
                OrderId = document.OrderId,
                CourierId = document.CourierId,
                Status = document.Status.ToString().ToLowerInvariant(),
                Volume = document.Volume,
                Weight = document.Weight,
                Source = new AddressDto
                {
                    Street = document.Source.Street,
                    BuildingNumber = document.Source.BuildingNumber,
                    ApartmentNumber = document.Source.ApartmentNumber,
                    City = document.Source.City,
                    Country = document.Source.Country,
                    ZipCode = document.Source.ZipCode
                },
                Destination = new AddressDto
                {
                    Street = document.Destination.Street,
                    BuildingNumber = document.Destination.BuildingNumber,
                    ApartmentNumber = document.Destination.ApartmentNumber,
                    City = document.Destination.City,
                    Country = document.Destination.Country,
                    ZipCode = document.Destination.ZipCode
                },
                Priority = document.Priority.ToString().ToLowerInvariant(),
                AtWeekend = document.AtWeekend,
                PickupDate = document.PickupDate,
                DeliveryDate = document.DeliveryDate,
                DeliveryAttemptDate = document.DeliveryAttemptDate,
                CannotDeliverReason = document.CannotDeliverReason,
                LastUpdate = document.LastUpdate
            };

    }
}