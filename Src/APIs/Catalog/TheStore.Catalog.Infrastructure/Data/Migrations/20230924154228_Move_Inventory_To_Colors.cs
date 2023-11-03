using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Move_Inventory_To_Colors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inventory_AvailableStock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Inventory_MaxStockThreshold",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Inventory_OnReorder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Inventory_OverStock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Inventory_RestockThreshold",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainImage",
                table: "ProductColor_images",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_AvailableStock",
                table: "ProductColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_MaxStockThreshold",
                table: "ProductColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Inventory_OnReorder",
                table: "ProductColor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_OverStock",
                table: "ProductColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_RestockThreshold",
                table: "ProductColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainColor",
                table: "ProductColor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Image_IsMainImage",
                table: "Branches",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainImage",
                table: "ProductColor_images");

            migrationBuilder.DropColumn(
                name: "Inventory_AvailableStock",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "Inventory_MaxStockThreshold",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "Inventory_OnReorder",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "Inventory_OverStock",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "Inventory_RestockThreshold",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "IsMainColor",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "Image_IsMainImage",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "Inventory_AvailableStock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_MaxStockThreshold",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Inventory_OnReorder",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_OverStock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_RestockThreshold",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
