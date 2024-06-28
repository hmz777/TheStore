using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimentions_Height",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Dimentions_Length",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Dimentions_Width",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Inventory_AvailableStock",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Inventory_MaxStockThreshold",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Inventory_OnReorder",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Inventory_OverStock",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Inventory_RestockThreshold",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Options_CanBeFavorited",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Options_CanBePurchased",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Options_IsMainVariant",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Options_Published",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Price_Amount",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "Price_Currency_CurrencyCode",
                table: "ProductVariants",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Dimentions_Unit_Unit",
                table: "ProductVariants",
                newName: "Options");

            migrationBuilder.AddColumn<string>(
                name: "Dimentions",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Inventory",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sizes",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimentions",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Sizes",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ProductVariants",
                newName: "Price_Currency_CurrencyCode");

            migrationBuilder.RenameColumn(
                name: "Options",
                table: "ProductVariants",
                newName: "Dimentions_Unit_Unit");

            migrationBuilder.AddColumn<decimal>(
                name: "Dimentions_Height",
                table: "ProductVariants",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dimentions_Length",
                table: "ProductVariants",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dimentions_Width",
                table: "ProductVariants",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_AvailableStock",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_MaxStockThreshold",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Inventory_OnReorder",
                table: "ProductVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_OverStock",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inventory_RestockThreshold",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Options_CanBeFavorited",
                table: "ProductVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Options_CanBePurchased",
                table: "ProductVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Options_IsMainVariant",
                table: "ProductVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Options_Published",
                table: "ProductVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price_Amount",
                table: "ProductVariants",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
