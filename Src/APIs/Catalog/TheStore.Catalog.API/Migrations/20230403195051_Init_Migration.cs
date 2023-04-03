﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    NeedsSynchronization = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SingleProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceAmount = table.Column<decimal>(name: "Price_Amount", type: "decimal(16,3)", precision: 16, scale: 3, nullable: false),
                    PriceCurrencyCurrencyCode = table.Column<string>(name: "Price_Currency_CurrencyCode", type: "nvarchar(max)", nullable: false),
                    InventoryAvailableStock = table.Column<int>(name: "Inventory_AvailableStock", type: "int", nullable: false),
                    InventoryRestockThreshold = table.Column<int>(name: "Inventory_RestockThreshold", type: "int", nullable: false),
                    InventoryMaxStockThreshold = table.Column<int>(name: "Inventory_MaxStockThreshold", type: "int", nullable: false),
                    InventoryOverStock = table.Column<int>(name: "Inventory_OverStock", type: "int", nullable: false),
                    InventoryOnReorder = table.Column<bool>(name: "Inventory_OnReorder", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductColor",
                columns: table => new
                {
                    SingleProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColor", x => new { x.SingleProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProductColor_SingleProducts_SingleProductId",
                        column: x => x.SingleProductId,
                        principalTable: "SingleProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssembledProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductId_SingleProducts_AssembledProductId",
                        column: x => x.AssembledProductId,
                        principalTable: "SingleProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductColor_Images",
                columns: table => new
                {
                    ProductColorSingleProductId = table.Column<int>(type: "int", nullable: false),
                    ProductColorId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColor_Images", x => new { x.ProductColorSingleProductId, x.ProductColorId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProductColor_Images_ProductColor_ProductColorSingleProductId_ProductColorId",
                        columns: x => new { x.ProductColorSingleProductId, x.ProductColorId },
                        principalTable: "ProductColor",
                        principalColumns: new[] { "SingleProductId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductId_AssembledProductId",
                table: "ProductId",
                column: "AssembledProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductColor_Images");

            migrationBuilder.DropTable(
                name: "ProductId");

            migrationBuilder.DropTable(
                name: "ProductColor");

            migrationBuilder.DropTable(
                name: "SingleProducts");
        }
    }
}
