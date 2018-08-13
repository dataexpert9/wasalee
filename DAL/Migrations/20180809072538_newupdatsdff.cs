using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class newupdatsdff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RatedAt",
                table: "DriverRating",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "DriverRating",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CancelItemReason",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<double>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    RatedAt = table.Column<DateTime>(nullable: false),
                    RequestItem_Id = table.Column<int>(nullable: false),
                    ReportProblemMessage_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelItemReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelItemReason_ReportProblemMessage_ReportProblemMessage_Id",
                        column: x => x.ReportProblemMessage_Id,
                        principalTable: "ReportProblemMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelItemReason_RequestItem_RequestItem_Id",
                        column: x => x.RequestItem_Id,
                        principalTable: "RequestItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_User_Id",
                table: "DriverRating",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CancelItemReason_ReportProblemMessage_Id",
                table: "CancelItemReason",
                column: "ReportProblemMessage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CancelItemReason_RequestItem_Id",
                table: "CancelItemReason",
                column: "RequestItem_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_Users_User_Id",
                table: "DriverRating",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_Users_User_Id",
                table: "DriverRating");

            migrationBuilder.DropTable(
                name: "CancelItemReason");

            migrationBuilder.DropIndex(
                name: "IX_DriverRating_User_Id",
                table: "DriverRating");

            migrationBuilder.DropColumn(
                name: "RatedAt",
                table: "DriverRating");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "DriverRating");
        }
    }
}
