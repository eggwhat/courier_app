using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Orders.Application.Commands
{
    public class AddParcelToOrder: ICommand
    {
        public Guid OrderId { get; }
        public Guid ParcelId { get; }
        public AddParcelToOrder(Guid orderId, Guid parcelId)
        {
            OrderId = orderId;
            ParcelId = parcelId;
        }
    }
}

