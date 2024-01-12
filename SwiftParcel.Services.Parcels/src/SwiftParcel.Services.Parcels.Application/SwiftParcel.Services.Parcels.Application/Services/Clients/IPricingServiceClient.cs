using SwiftParcel.Services.Parcels.Application.DTO;

namespace SwiftParcel.Services.Parcels.Application.Services.Clients
{
    public interface IPricingServiceClient
    {
        Task<ParcelDeliveryPricingDto> GetParcelDeliveryPriceAsync(Guid parcelId, decimal calculatedPrice,
        double length, double width, double height, double weight, bool highPriority, bool deliverAtWeekend,
        bool vipPackage);
    }
}