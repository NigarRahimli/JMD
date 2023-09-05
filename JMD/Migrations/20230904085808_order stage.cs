using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JMD.Migrations
{
    /// <inheritdoc />
    public partial class orderstage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StageLevel",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "BlogLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StageLevel",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "BlogLanguages");
        }
    }
}
