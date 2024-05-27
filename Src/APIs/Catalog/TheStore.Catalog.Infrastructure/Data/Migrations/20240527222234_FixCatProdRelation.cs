using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
	/// <inheritdoc />
	public partial class FixCatProdRelation : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ProductSpecification_ProductVariant_ProductVariantID",
				table: "ProductSpecification");

			migrationBuilder.DropForeignKey(
				name: "FK_ProductVariant_Products_ProductId",
				table: "ProductVariant");

			migrationBuilder.DropIndex(
				name: "IX_Products_CategoryId",
				table: "Products");

			migrationBuilder.AlterColumn<int>(
				name: "ProductId",
				table: "ProductVariant",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "ProductVariantID",
				table: "ProductSpecification",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");

			migrationBuilder.AddForeignKey(
				name: "FK_ProductSpecification_ProductVariant_ProductVariantID",
				table: "ProductSpecification",
				column: "ProductVariantID",
				principalTable: "ProductVariant",
				principalColumn: "ID",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_ProductVariant_Products_ProductId",
				table: "ProductVariant",
				column: "ProductId",
				principalTable: "Products",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ProductSpecification_ProductVariant_ProductVariantID",
				table: "ProductSpecification");

			migrationBuilder.DropForeignKey(
				name: "FK_ProductVariant_Products_ProductId",
				table: "ProductVariant");

			migrationBuilder.DropIndex(
				name: "IX_Products_CategoryId",
				table: "Products");

			migrationBuilder.AlterColumn<int>(
				name: "ProductId",
				table: "ProductVariant",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "ProductVariantID",
				table: "ProductSpecification",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_ProductSpecification_ProductVariant_ProductVariantID",
				table: "ProductSpecification",
				column: "ProductVariantID",
				principalTable: "ProductVariant",
				principalColumn: "ID");

			migrationBuilder.AddForeignKey(
				name: "FK_ProductVariant_Products_ProductId",
				table: "ProductVariant",
				column: "ProductId",
				principalTable: "Products",
				principalColumn: "Id");
		}
	}
}
