namespace SwiftParcel.ExternalAPI.Baronomat.Core.Mappers
{
    public static class OrderStateToStatusMapper
    {
        public static string Convert(string state)
        {
            switch (state)
            {
                case "Created":
                    return "WaitingForDecision";
                case "Accepted":
                    return "Confirmed";
                case "Rejected":
                    return "Cancelled";
                case "Completed":
                    return "Delivered";
                default:
                    return "Accepted";
            }
        }
    }
}