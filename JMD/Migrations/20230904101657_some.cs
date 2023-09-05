using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JMD.Migrations
{
    /// <inheritdoc />
    public partial class some : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "BlogLanguages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "BlogLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
