namespace SwiftParcel.ExternalAPI.Baronomat.Application.Events.Rejected
{
    public class AddParcelRejected
    {
        public Guid ParcelId { get; }
        public string Reason { get; }
        public string Code { get; }

        public AddParcelRejected(Guid parcelId, string reason, string code)
        {
            ParcelId = parcelId;
            Reason = reason;
            Code = code;
        }
    }
}