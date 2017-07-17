using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DeerflyPatches.Models;

namespace DeerflyPatches.Migrations
{
    [DbContext(typeof(DeerflyPatchesContext))]
    [Migration("20170717170028_Products")]
    partial class Products
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeerflyPatches.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<DateTime>("LastVisited");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Registered");

                    b.Property<int>("TimesVisited");

                    b.HasKey("ID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("DeerflyPatches.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("ImageURL");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Shipping");

                    b.HasKey("ID");

                    b.ToTable("Product");
                });
        }
    }
}
