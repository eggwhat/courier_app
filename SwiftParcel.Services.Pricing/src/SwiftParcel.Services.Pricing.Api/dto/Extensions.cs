using SwiftParcel.Services.Pricing.Api.Core.Entities;

namespace SwiftParcel.Services.Pricing.Api.dto
{
    internal static class Extensions
    {
        public static Customer AsEntity(this CustomerDto dto)
            => new Customer(dto.Id, dto.IsVip, dto.CompletedOrders.Count());

        public static Parcel AsEntity(this ParcelDto dto)
        {
            return new Parcel(dto.Length, dto.Width, dto.Height, dto.Weight, 
                dto.HighPriority, dto.DeliverAtWeekend, dto.VipPackage);
        }
    }
}