using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProductColorFieldsMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "ProductColor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "ProductColor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainColor",
                table: "ProductColor",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "IsMainColor",
                table: "ProductColor");
        }
    }
}
