using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DeerflyPatches.Models;

namespace DeerflyPatches.Migrations
{
    [DbContext(typeof(DeerflyPatchesContext))]
    [Migration("20170724214144_Promo")]
    partial class Promo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeerflyPatches.Models.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<int?>("OwnerID");

                    b.Property<string>("Phone");

                    b.Property<string>("Recipient");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Address");
                });

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

            modelBuilder.Entity("DeerflyPatches.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BillToID");

                    b.Property<DateTime>("DateOrdered");

                    b.Property<int?>("PurchaserID");

                    b.Property<int?>("ShipToID");

                    b.Property<decimal>("Shipping");

                    b.Property<decimal>("Subtotal");

                    b.Property<decimal>("Total");

                    b.HasKey("ID");

                    b.HasIndex("BillToID");

                    b.HasIndex("PurchaserID");

                    b.HasIndex("ShipToID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DeerflyPatches.Models.OrderDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CheckedOut");

                    b.Property<int?>("ItemID");

                    b.Property<int?>("OrderID");

                    b.Property<DateTime>("PlacedInCart");

                    b.Property<int?>("PurchaserID");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("Shipping");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("ID");

                    b.HasIndex("ItemID");

                    b.HasIndex("OrderID");

                    b.HasIndex("PurchaserID");

                    b.ToTable("OrderDetail");
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

            modelBuilder.Entity("DeerflyPatches.Models.Address", b =>
                {
                    b.HasOne("DeerflyPatches.Models.Customer", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID");
                });

            modelBuilder.Entity("DeerflyPatches.Models.Order", b =>
                {
                    b.HasOne("DeerflyPatches.Models.Address", "BillTo")
                        .WithMany()
                        .HasForeignKey("BillToID");

                    b.HasOne("DeerflyPatches.Models.Customer", "Purchaser")
                        .WithMany()
                        .HasForeignKey("PurchaserID");

                    b.HasOne("DeerflyPatches.Models.Address", "ShipTo")
                        .WithMany()
                        .HasForeignKey("ShipToID");
                });

            modelBuilder.Entity("DeerflyPatches.Models.OrderDetail", b =>
                {
                    b.HasOne("DeerflyPatches.Models.Product", "Item")
                        .WithMany()
                        .HasForeignKey("ItemID");

                    b.HasOne("DeerflyPatches.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID");

                    b.HasOne("DeerflyPatches.Models.Customer", "Purchaser")
                        .WithMany()
                        .HasForeignKey("PurchaserID");
                });
        }
    }
}
