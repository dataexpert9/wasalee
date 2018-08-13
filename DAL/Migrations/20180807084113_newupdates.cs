using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class newupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DropOffLatitude",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DropOffLongitude",
                table: "RequestItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffLatitude",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "DropOffLongitude",
                table: "RequestItem");
        }
    }
}
