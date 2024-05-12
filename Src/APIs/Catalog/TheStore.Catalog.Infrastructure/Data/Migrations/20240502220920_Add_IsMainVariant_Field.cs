using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsMainVariant_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Options_IsMainVariant",
                table: "ProductVariant",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Options_IsMainVariant",
                table: "ProductVariant");
        }
    }
}
