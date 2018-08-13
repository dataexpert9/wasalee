using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class cancel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CancelItemReason");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "CancelItemReason");

            migrationBuilder.RenameColumn(
                name: "RatedAt",
                table: "CancelItemReason",
                newName: "CancelAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CancelAt",
                table: "CancelItemReason",
                newName: "RatedAt");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "CancelItemReason",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "CancelItemReason",
                nullable: true);
        }
    }
}
