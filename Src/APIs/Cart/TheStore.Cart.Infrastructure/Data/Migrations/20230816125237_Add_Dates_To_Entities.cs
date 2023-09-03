using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Cart.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Dates_To_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemId");

            migrationBuilder.DropTable(
                name: "WishlistItemId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistItem",
                table: "WishlistItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Wishlists",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Wishlists",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "WishlistId",
                table: "WishlistItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "WishlistItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Carts",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Carts",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "CartItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Buyers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Buyers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Buyers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Buyers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistItem",
                table: "WishlistItem",
                columns: new[] { "WishlistId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                columns: new[] { "CartId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItem_Wishlists_WishlistId",
                table: "WishlistItem",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItem_Wishlists_WishlistId",
                table: "WishlistItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistItem",
                table: "WishlistItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "WishlistItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishlistItem");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Buyers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistItem",
                table: "WishlistItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CartItemId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemId_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WishlistItemId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItemId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItemId_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemId_CartId",
                table: "CartItemId",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItemId_WishlistId",
                table: "WishlistItemId",
                column: "WishlistId");
        }
    }
}
