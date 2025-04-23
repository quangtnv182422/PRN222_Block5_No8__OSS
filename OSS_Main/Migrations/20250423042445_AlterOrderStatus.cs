using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSS_Main.Migrations
{
    /// <inheritdoc />
    public partial class AlterOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                newName: "OrderStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatusName",
                table: "OrderStatuses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "OrderDisplay",
                table: "OrderStatuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "OrderDisplay",
                table: "OrderStatuses");

            migrationBuilder.RenameTable(
                name: "OrderStatuses",
                newName: "OrderStatus");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatusName",
                table: "OrderStatus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
