using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ratingupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportProblemMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportProblemMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverRating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<double>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    Driver_Id = table.Column<int>(nullable: false),
                    ReportProblemMessage_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverRating_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRating_ReportProblemMessage_ReportProblemMessage_Id",
                        column: x => x.ReportProblemMessage_Id,
                        principalTable: "ReportProblemMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_Driver_Id",
                table: "DriverRating",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_ReportProblemMessage_Id",
                table: "DriverRating",
                column: "ReportProblemMessage_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverRating");

            migrationBuilder.DropTable(
                name: "ReportProblemMessage");
        }
    }
}
