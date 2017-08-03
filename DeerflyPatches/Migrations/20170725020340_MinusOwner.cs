using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeerflyPatches.Migrations
{
    public partial class MinusOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_OwnerID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_OwnerID",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_OwnerID",
                table: "Address",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_OwnerID",
                table: "Address",
                column: "OwnerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
