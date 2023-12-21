using Convey.CQRS.Queries;
using SwiftParcel.Services.Deliveries.Application.DTO;

namespace SwiftParcel.Services.Deliveries.Application.Queries
{
    public class GetDeliveries : IQuery<IEnumerable<DeliveryDto>>
    {
        public Guid CourierId { get; set; }
    }
}