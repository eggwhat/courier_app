using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.OrdersCreator.Sagas
{
    public class AIMakingOrderData
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CourierId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ReservationPriority { get; set; }
        public List<Guid> ParcelIds { get; set; } = new List<Guid>();
        public List<Guid> AddedParcelIds { get; set; } = new List<Guid>();
        public bool AllPackagesAddedToOrder => AddedParcelIds.Any() && AddedParcelIds.All(ParcelIds.Contains);
    }
}