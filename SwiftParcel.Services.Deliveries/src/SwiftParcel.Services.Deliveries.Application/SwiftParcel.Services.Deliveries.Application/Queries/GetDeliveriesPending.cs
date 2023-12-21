using Convey.CQRS.Queries;
using SwiftParcel.Services.Deliveries.Application.DTO;

namespace SwiftParcel.Services.Deliveries.Application.Queries
{
    public class GetDeliveriesPending : IQuery<IEnumerable<DeliveryDto>>
    {
        
    }
}