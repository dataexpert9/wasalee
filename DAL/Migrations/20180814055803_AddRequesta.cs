using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddRequesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_RequestItem_Request_Id",
                table: "DriverRating");

            migrationBuilder.AlterColumn<int>(
                name: "Request_Id",
                table: "DriverRating",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_RequestItem_Request_Id",
                table: "DriverRating",
                column: "Request_Id",
                principalTable: "RequestItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_RequestItem_Request_Id",
                table: "DriverRating");

            migrationBuilder.AlterColumn<int>(
                name: "Request_Id",
                table: "DriverRating",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_RequestItem_Request_Id",
                table: "DriverRating",
                column: "Request_Id",
                principalTable: "RequestItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
