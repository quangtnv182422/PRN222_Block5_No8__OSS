using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSS_Main.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultInReceiver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ReceiverInformations",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ReceiverInformations");
        }
    }
}
