using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class DeleteParcelFromOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid ParcelId { get; }
        public DeleteParcelFromOrder(Guid orderId, Guid parcelId)
        {
            OrderId = orderId;
            ParcelId = parcelId;
        }
    }
}


