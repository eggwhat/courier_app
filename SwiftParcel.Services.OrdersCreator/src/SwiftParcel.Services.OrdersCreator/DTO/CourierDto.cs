using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.OrdersCreator.DTO
{
    public class CourierDto
    {
        public Guid Id { get; set; }
        //public string Brand { get; set; }
        //public string Model { get; set; }
        public string CourierName { get; set; }
        public decimal PricePerService { get; set; }
    }
}