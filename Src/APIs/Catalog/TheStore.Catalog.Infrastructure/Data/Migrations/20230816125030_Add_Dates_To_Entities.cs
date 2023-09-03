using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Dates_To_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_SingleProducts_SingleProductId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_images_ProductColor_ProductColorSingleProductId_ProductColorId",
                table: "ProductColor_images");

            migrationBuilder.DropTable(
                name: "ProductId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleProducts",
                table: "SingleProducts");

            migrationBuilder.RenameTable(
                name: "SingleProducts",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductColorSingleProductId",
                table: "ProductColor_images",
                newName: "ProductColorProductId");

            migrationBuilder.RenameColumn(
                name: "SingleProductId",
                table: "ProductColor",
                newName: "ProductId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Categories",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Categories",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<string>(
                name: "Image_StringFileUri",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Image_Alt",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Branches",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Branches",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Products",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Products",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_images_ProductColor_ProductColorProductId_ProductColorId",
                table: "ProductColor_images",
                columns: new[] { "ProductColorProductId", "ProductColorId" },
                principalTable: "ProductColor",
                principalColumns: new[] { "ProductId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_images_ProductColor_ProductColorProductId_ProductColorId",
                table: "ProductColor_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "SingleProducts");

            migrationBuilder.RenameColumn(
                name: "ProductColorProductId",
                table: "ProductColor_images",
                newName: "ProductColorSingleProductId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductColor",
                newName: "SingleProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Image_StringFileUri",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image_Alt",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleProducts",
                table: "SingleProducts",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductId_AssembledProductId",
                table: "ProductId",
                column: "AssembledProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_SingleProducts_SingleProductId",
                table: "ProductColor",
                column: "SingleProductId",
                principalTable: "SingleProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_images_ProductColor_ProductColorSingleProductId_ProductColorId",
                table: "ProductColor_images",
                columns: new[] { "ProductColorSingleProductId", "ProductColorId" },
                principalTable: "ProductColor",
                principalColumns: new[] { "SingleProductId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
