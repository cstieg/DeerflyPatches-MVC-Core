using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeerflyPatches.Models
{
    public class DeerflyPatchesContext : DbContext
    {
        public DeerflyPatchesContext (DbContextOptions<DeerflyPatchesContext> options)
            : base(options)
        {
        }

        public DbSet<DeerflyPatches.Models.Customer> Customer { get; set; }
    }
}
