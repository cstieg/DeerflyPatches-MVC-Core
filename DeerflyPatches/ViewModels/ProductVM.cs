using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeerflyPatches.ViewModels
{
    public class ProductVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Shipping { get; set; }
        public IFormFile ImageUpload { get; set; }
        public string Category { get; set; }
    }
}
