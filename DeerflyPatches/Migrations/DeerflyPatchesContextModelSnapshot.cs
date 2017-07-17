﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DeerflyPatches.Models;

namespace DeerflyPatches.Migrations
{
    [DbContext(typeof(DeerflyPatchesContext))]
    partial class DeerflyPatchesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
        }
    }
}
