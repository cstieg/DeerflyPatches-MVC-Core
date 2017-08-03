using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DeerflyPatches.Migrations
{
    public partial class Promo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromoCode",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CodeEnd = table.Column<DateTime>(nullable: false),
                    CodeStart = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MinimumQualifyingPurchase = table.Column<decimal>(nullable: false),
                    PercentOffItem = table.Column<decimal>(nullable: false),
                    PercentOffOrder = table.Column<decimal>(nullable: false),
                    PromotionalItemID = table.Column<int>(nullable: true),
                    PromotionalItemPrice = table.Column<decimal>(nullable: false),
                    SpecialPrice = table.Column<decimal>(nullable: false),
                    SpecialPriceItemID = table.Column<int>(nullable: true),
                    WithPurchaseOfID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCode", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromoCode_Product_PromotionalItemID",
                        column: x => x.PromotionalItemID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromoCode_Product_SpecialPriceItemID",
                        column: x => x.SpecialPriceItemID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromoCode_Product_WithPurchaseOfID",
                        column: x => x.WithPurchaseOfID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_PromotionalItemID",
                table: "PromoCode",
                column: "PromotionalItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_SpecialPriceItemID",
                table: "PromoCode",
                column: "SpecialPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_WithPurchaseOfID",
                table: "PromoCode",
                column: "WithPurchaseOfID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromoCode");
        }
    }
}
