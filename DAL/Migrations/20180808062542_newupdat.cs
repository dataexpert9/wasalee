using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class newupdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RequestItemML",
                newName: "ItemDescription");

            migrationBuilder.AddColumn<string>(
                name: "ItemDescription",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "RequestItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemDescription",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RequestItem");

            migrationBuilder.RenameColumn(
                name: "ItemDescription",
                table: "RequestItemML",
                newName: "Description");
        }
    }
}
