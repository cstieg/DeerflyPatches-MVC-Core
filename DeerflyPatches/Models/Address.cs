using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeerflyPatches.Models
{
    public class Address
    {
        public int ID { get; set; }
        public Customer Owner { get; set; }
        public string Recipient { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public AddressType Type { get; set; }
    }

    public enum AddressType
    {
        Billing,
        Shipping
    }
}