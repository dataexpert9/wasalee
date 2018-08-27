using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Request_Id",
                table: "DriverRating",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_Request_Id",
                table: "DriverRating",
                column: "Request_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_RequestItem_Request_Id",
                table: "DriverRating",
                column: "Request_Id",
                principalTable: "RequestItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_RequestItem_Request_Id",
                table: "DriverRating");

            migrationBuilder.DropIndex(
                name: "IX_DriverRating_Request_Id",
                table: "DriverRating");

            migrationBuilder.DropColumn(
                name: "Request_Id",
                table: "DriverRating");
        }
    }
}
