namespace SwiftParcel.Services.Orders.Core.Entities
{
    public enum OrderStatus
    {
        WaitingForDecision,
        Approved,
        Confirmed,
        Cancelled,
        PickedUp,
        Delivered,
        CannotDeliver,

    }
}

