using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JMD.Migrations
{
    /// <inheritdoc />
    public partial class nese : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StageName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StageName",
                table: "Orders");
        }
    }
}
