namespace SwiftParcel.ExternalAPI.Baronomat.Application.DTO
{
    public class PriceRequestDto
    {
        public int ShipmentWidthMm { get; set; }
        public int ShipmentHeightMm { get; set; }
        public int ShipmentLengthMm { get; set; }
        public int ShipmentWeightMg { get; set;  }
        public string DeliveryDate { get; set; }
        public bool HighPriority { get; set; } 
        public bool WeekendDelivery { get; set; }

        public PriceRequestDto(double shipmentWidthMm, double shipmentHeightMm, double shipmentLengthMm, double shipmentWeightMg, 
            DateTime deliveryDate, string highPriority, bool weekendDelivery)
        {
            ShipmentWidthMm = (int)shipmentWidthMm;
            ShipmentHeightMm = (int)shipmentHeightMm;
            ShipmentLengthMm = (int)shipmentLengthMm;
            ShipmentWeightMg = (int)shipmentWeightMg;
            DeliveryDate = deliveryDate.ToString("yyyy-MM-dd");   
            HighPriority = highPriority == "High";
            WeekendDelivery = weekendDelivery;
        }
    }
}