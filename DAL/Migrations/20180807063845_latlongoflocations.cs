using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class latlongoflocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PickUpLatitude",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PickUpLongitude",
                table: "RequestItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickUpLatitude",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "PickUpLongitude",
                table: "RequestItem");
        }
    }
}
