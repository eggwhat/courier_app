using System;
using System.Collections.Generic;
using Convey.Types;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Infrastructure.Mongo.Documents
{
    public class OrderDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Parcel Parcel { get; set; }
        public OrderStatus Status { get; set; }
        public Company CourierCompany { get; set; }
        public DateTime OrderRequestDate { get; set; }
        public DateTime RequestValidTo { get; set;}
        public string BuyerName { get; set; }
        public string BuyerEmail { get; set; }
        public Address BuyerAddress { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? PickedUpAt { get; set; } 
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CannotDeliverAt { get; set; }
        public string CancellationReason { get; set; }
        public string CannotDeliverReason { get; set; }
    }
}
