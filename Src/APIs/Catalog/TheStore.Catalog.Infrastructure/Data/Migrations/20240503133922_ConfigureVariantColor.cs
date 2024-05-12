using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureVariantColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_ProductColor_ProductColorID",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_ProductColor_ColorID",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariant_ColorID",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "ColorID",
                table: "ProductVariant");

            migrationBuilder.AddColumn<int>(
                name: "variantId",
                table: "ProductColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductColorID",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_variantId",
                table: "ProductColor",
                column: "variantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_ProductColor_ProductColorID",
                table: "Image",
                column: "ProductColorID",
                principalTable: "ProductColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_ProductVariant_variantId",
                table: "ProductColor",
                column: "variantId",
                principalTable: "ProductVariant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_ProductColor_ProductColorID",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_ProductVariant_variantId",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_variantId",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "variantId",
                table: "ProductColor");

            migrationBuilder.AddColumn<int>(
                name: "ColorID",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductColorID",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ColorID",
                table: "ProductVariant",
                column: "ColorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_ProductColor_ProductColorID",
                table: "Image",
                column: "ProductColorID",
                principalTable: "ProductColor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_ProductColor_ColorID",
                table: "ProductVariant",
                column: "ColorID",
                principalTable: "ProductColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
