using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class flagupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUserRated",
                table: "RequestItem",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUserRated",
                table: "RequestItem");
        }
    }
}
