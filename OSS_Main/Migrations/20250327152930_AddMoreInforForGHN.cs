using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSS_Main.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreInforForGHN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistrictName_GHN",
                table: "ReceiverInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceName_GHN",
                table: "ReceiverInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardName_GHN",
                table: "ReceiverInformations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictName_GHN",
                table: "ReceiverInformations");

            migrationBuilder.DropColumn(
                name: "ProvinceName_GHN",
                table: "ReceiverInformations");

            migrationBuilder.DropColumn(
                name: "WardName_GHN",
                table: "ReceiverInformations");
        }
    }
}
