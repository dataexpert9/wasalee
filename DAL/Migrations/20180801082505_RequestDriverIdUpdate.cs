using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RequestDriverIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Driver_Id",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestItem_Driver_Id",
                table: "RequestItem",
                column: "Driver_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestItem_Drivers_Driver_Id",
                table: "RequestItem",
                column: "Driver_Id",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestItem_Drivers_Driver_Id",
                table: "RequestItem");

            migrationBuilder.DropIndex(
                name: "IX_RequestItem_Driver_Id",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "Driver_Id",
                table: "RequestItem");
        }
    }
}
