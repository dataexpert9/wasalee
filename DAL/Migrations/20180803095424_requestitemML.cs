using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class requestitemML : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "DropOffLocation",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "PickUpLocation",
                table: "RequestItem");

            migrationBuilder.CreateTable(
                name: "RequestItemML",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PickUpLocation = table.Column<string>(nullable: true),
                    DropOffLocation = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    RequestItem_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItemML", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItemML_RequestItem_RequestItem_Id",
                        column: x => x.RequestItem_Id,
                        principalTable: "RequestItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestItemML_RequestItem_Id",
                table: "RequestItemML",
                column: "RequestItem_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestItemML");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DropOffLocation",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RequestItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpLocation",
                table: "RequestItem",
                nullable: true);
        }
    }
}
