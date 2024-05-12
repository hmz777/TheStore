using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixImageObjectMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alt_Json",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainImage",
                table: "Image",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StringFileUri",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alt_Json",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "IsMainImage",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "StringFileUri",
                table: "Image");
        }
    }
}
