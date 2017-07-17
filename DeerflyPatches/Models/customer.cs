using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeerflyPatches.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Registered { get; set; }
        public DateTime LastVisited { get; set; }
        public int TimesVisited { get; set; }
        public string EmailAddress { get; set; }
    }
}
