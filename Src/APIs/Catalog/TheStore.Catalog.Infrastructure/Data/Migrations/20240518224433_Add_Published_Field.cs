using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Published_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_ProductVariant_ProductVariantID",
                table: "ProductReview");

            migrationBuilder.RenameColumn(
                name: "ProductVariantID",
                table: "ProductReview",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_ProductVariantID",
                table: "ProductReview",
                newName: "IX_ProductReview_ProductId");

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "ProductReview",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Products_ProductId",
                table: "ProductReview",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Products_ProductId",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "ProductReview");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductReview",
                newName: "ProductVariantID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                newName: "IX_ProductReview_ProductVariantID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_ProductVariant_ProductVariantID",
                table: "ProductReview",
                column: "ProductVariantID",
                principalTable: "ProductVariant",
                principalColumn: "ID");
        }
    }
}
