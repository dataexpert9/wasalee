using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ratingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_Users_UserId",
                table: "DriverRating");

            migrationBuilder.DropIndex(
                name: "IX_DriverRating_UserId",
                table: "DriverRating");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DriverRating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DriverRating",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_UserId",
                table: "DriverRating",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_Users_UserId",
                table: "DriverRating",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
