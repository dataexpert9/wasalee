using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_Driver_Id",
                table: "DriverRating",
                column: "Driver_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_Drivers_Driver_Id",
                table: "DriverRating",
                column: "Driver_Id",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_Drivers_Driver_Id",
                table: "DriverRating");

            migrationBuilder.DropIndex(
                name: "IX_DriverRating_Driver_Id",
                table: "DriverRating");

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "DriverRating",
                nullable: true);

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
    }
}
