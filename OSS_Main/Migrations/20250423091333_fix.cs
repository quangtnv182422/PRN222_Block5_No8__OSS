using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSS_Main.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Products_ProductId",
                table: "Medias");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Medias",
                newName: "FeedbackId");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_ProductId",
                table: "Medias",
                newName: "IX_Medias_FeedbackId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Feedbacks_FeedbackId",
                table: "Medias",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Feedbacks_FeedbackId",
                table: "Medias");

            migrationBuilder.RenameColumn(
                name: "FeedbackId",
                table: "Medias",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_FeedbackId",
                table: "Medias",
                newName: "IX_Medias_ProductId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Products_ProductId",
                table: "Medias",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
