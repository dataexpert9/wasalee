using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class driverRatingDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "DriverRating",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Driver_Id",
                table: "DriverRating",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_DriverId",
                table: "DriverRating",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_Drivers_DriverId",
                table: "DriverRating",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_Drivers_DriverId",
                table: "DriverRating");

            migrationBuilder.DropIndex(
                name: "IX_DriverRating_DriverId",
                table: "DriverRating");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "DriverRating");

            migrationBuilder.DropColumn(
                name: "Driver_Id",
                table: "DriverRating");
        }
    }
}
