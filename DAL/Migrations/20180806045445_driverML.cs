using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class driverML : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "Drivers");

            migrationBuilder.CreateTable(
                name: "DriverML",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(nullable: true),
                    HomeAddress = table.Column<string>(nullable: true),
                    BriefInfo = table.Column<string>(nullable: true),
                    WorkHistory = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    Driver_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverML", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverML_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverML_Driver_Id",
                table: "DriverML",
                column: "Driver_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverML");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "Drivers",
                nullable: true);
        }
    }
}
