using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeerflyPatches.Models;

namespace DeerflyPatches.Models
{
    public class DeerflyPatchesContext : DbContext
    {
        public DeerflyPatchesContext (DbContextOptions<DeerflyPatchesContext> options)
            : base(options)
        {
        }

        public DbSet<DeerflyPatches.Models.Customer> Customer { get; set; }

        public DbSet<DeerflyPatches.Models.Product> Product { get; set; }

        public DbSet<DeerflyPatches.Models.Address> Address { get; set; }

        public DbSet<DeerflyPatches.Models.Order> Order { get; set; }
    }
}
