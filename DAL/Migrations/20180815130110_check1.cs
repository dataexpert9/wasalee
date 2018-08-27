using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class check1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "DriverRating",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_User_Id",
                table: "DriverRating",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_Users_User_Id",
                table: "DriverRating",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_Users_User_Id",
                table: "DriverRating");

            migrationBuilder.DropIndex(
                name: "IX_DriverRating_User_Id",
                table: "DriverRating");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "DriverRating");
        }
    }
}
