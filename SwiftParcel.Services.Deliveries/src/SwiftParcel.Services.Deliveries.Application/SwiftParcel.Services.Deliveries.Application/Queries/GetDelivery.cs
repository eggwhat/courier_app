using Convey.CQRS.Queries;
using SwiftParcel.Services.Deliveries.Application.DTO;

namespace SwiftParcel.Services.Deliveries.Application.Queries
{
    public class GetDelivery : IQuery<DeliveryDto>
    { 
        public Guid DeliveryId { get; set; }
    }
}