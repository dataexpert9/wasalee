using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PrivacyPolicy",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TermsOfUse",
                table: "Settings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUs",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivacyPolicy",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermsOfUse",
                table: "Settings",
                nullable: true);
        }
    }
}
