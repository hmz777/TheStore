using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Cart.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceProductIdWithSkuInCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishlistItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartItem");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "WishlistItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "CartItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sku",
                table: "WishlistItem");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "CartItem");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "WishlistItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
