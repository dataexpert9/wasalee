using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UserIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuisine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    HomeAddress = table.Column<string>(nullable: true),
                    LicenseNo = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    IsNotificationsOn = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    SignInType = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AboutUs = table.Column<string>(nullable: true),
                    PrivacyPolicy = table.Column<string>(nullable: true),
                    TermsOfUse = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Open_From = table.Column<TimeSpan>(nullable: false),
                    Open_To = table.Column<TimeSpan>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    AverageDeliveryTime = table.Column<int>(nullable: false),
                    MinOrder = table.Column<float>(nullable: true),
                    DeliveryFee = table.Column<double>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFeature = table.Column<bool>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    Location_Longitude = table.Column<double>(nullable: false),
                    Location_Latitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ProfilePictureUrl = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    SignInType = table.Column<int>(nullable: true),
                    Status = table.Column<short>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PhoneConfirmed = table.Column<bool>(nullable: false),
                    IsNotificationsOn = table.Column<bool>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreCuisine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Store_Id = table.Column<int>(nullable: false),
                    Cuisine_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCuisine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreCuisine_Cuisine_Cuisine_Id",
                        column: x => x.Cuisine_Id,
                        principalTable: "Cuisine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreCuisine_Store_Store_Id",
                        column: x => x.Store_Id,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreRating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<double>(nullable: false),
                    Feedback = table.Column<string>(nullable: true),
                    Store_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreRating_Store_Store_Id",
                        column: x => x.Store_Id,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreTiming",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Monday_From = table.Column<TimeSpan>(nullable: false),
                    Monday_To = table.Column<TimeSpan>(nullable: false),
                    Tuesday_From = table.Column<TimeSpan>(nullable: false),
                    Tuesday_To = table.Column<TimeSpan>(nullable: false),
                    Wednesday_From = table.Column<TimeSpan>(nullable: false),
                    Wednesday_To = table.Column<TimeSpan>(nullable: false),
                    Thursday_From = table.Column<TimeSpan>(nullable: false),
                    Thursday_To = table.Column<TimeSpan>(nullable: false),
                    Friday_From = table.Column<TimeSpan>(nullable: false),
                    Friday_To = table.Column<TimeSpan>(nullable: false),
                    Saturday_From = table.Column<TimeSpan>(nullable: false),
                    Saturday_To = table.Column<TimeSpan>(nullable: false),
                    Sunday_From = table.Column<TimeSpan>(nullable: false),
                    Sunday_To = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTiming", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreTiming_Store_Id",
                        column: x => x.Id,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    PriceRangeFrom = table.Column<double>(nullable: false),
                    PriceRangeTo = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    DeliveryTime = table.Column<TimeSpan>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PickUpLocation = table.Column<string>(nullable: true),
                    DropOffLocation = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItem_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceName = table.Column<string>(nullable: true),
                    UDID = table.Column<string>(nullable: true),
                    Platform = table.Column<bool>(nullable: false),
                    User_Id = table.Column<int>(nullable: true),
                    AuthToken = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDevice_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestItemImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageUrl = table.Column<string>(nullable: true),
                    RequestItem_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItemImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItemImages_RequestItem_RequestItem_Id",
                        column: x => x.RequestItem_Id,
                        principalTable: "RequestItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestItem_User_Id",
                table: "RequestItem",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItemImages_RequestItem_Id",
                table: "RequestItemImages",
                column: "RequestItem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCuisine_Cuisine_Id",
                table: "StoreCuisine",
                column: "Cuisine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCuisine_Store_Id",
                table: "StoreCuisine",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRating_Store_Id",
                table: "StoreRating",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevice_UserId",
                table: "UserDevice",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "RequestItemImages");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "StoreCuisine");

            migrationBuilder.DropTable(
                name: "StoreRating");

            migrationBuilder.DropTable(
                name: "StoreTiming");

            migrationBuilder.DropTable(
                name: "UserDevice");

            migrationBuilder.DropTable(
                name: "RequestItem");

            migrationBuilder.DropTable(
                name: "Cuisine");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
