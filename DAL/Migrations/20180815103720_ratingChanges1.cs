using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ratingChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverRating_Id",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestItem_DriverRating_Id",
                table: "RequestItem",
                column: "DriverRating_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestItem_DriverRating_DriverRating_Id",
                table: "RequestItem",
                column: "DriverRating_Id",
                principalTable: "DriverRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestItem_DriverRating_DriverRating_Id",
                table: "RequestItem");

            migrationBuilder.DropIndex(
                name: "IX_RequestItem_DriverRating_Id",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "DriverRating_Id",
                table: "RequestItem");
        }
    }
}
