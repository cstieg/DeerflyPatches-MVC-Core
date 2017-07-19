using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeerflyPatches.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Customer Purchaser { get; set; }
        public DateTime DateOrdered { get; set; }
        public Address ShipTo { get; set; }
        public Address BillTo { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
    }
}
