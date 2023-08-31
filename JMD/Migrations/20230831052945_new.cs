using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JMD.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "BlogLanguages");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OrderType",
                newName: "OrderName");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "BlogLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "BlogLanguages");

            migrationBuilder.RenameColumn(
                name: "OrderName",
                table: "OrderType",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "BlogLanguages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
