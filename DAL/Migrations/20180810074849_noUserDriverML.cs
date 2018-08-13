using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class noUserDriverML : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverML");

            migrationBuilder.DropTable(
                name: "UserML");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BriefInfo",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkHistory",
                table: "Drivers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BriefInfo",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "WorkHistory",
                table: "Drivers");

            migrationBuilder.CreateTable(
                name: "DriverML",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BriefInfo = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    Driver_Id = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    HomeAddress = table.Column<string>(nullable: true),
                    WorkHistory = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UserML",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserML", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserML_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverML_Driver_Id",
                table: "DriverML",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserML_User_Id",
                table: "UserML",
                column: "User_Id");
        }
    }
}
