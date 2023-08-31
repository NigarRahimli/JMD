using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JMD.Migrations
{
    /// <inheritdoc />
    public partial class new2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderType_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderType",
                table: "OrderType");

            migrationBuilder.RenameTable(
                name: "OrderType",
                newName: "OrderTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTypes",
                table: "OrderTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId",
                principalTable: "OrderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTypes",
                table: "OrderTypes");

            migrationBuilder.RenameTable(
                name: "OrderTypes",
                newName: "OrderType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderType",
                table: "OrderType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderType_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId",
                principalTable: "OrderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
