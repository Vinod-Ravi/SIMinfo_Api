using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMinfo.API.Migrations
{
    public partial class SimInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileCountryCode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    CodeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileCountryCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdviceOfCharge = table.Column<string>(type: "text", nullable: true),
                    AuthenticationKey = table.Column<string>(type: "text", nullable: true),
                    MobileCountryCode = table.Column<string>(type: "text", nullable: true),
                    LocalAreaIdentity = table.Column<string>(type: "text", nullable: true),
                    ServiceProviderName = table.Column<string>(type: "text", nullable: true),
                    IntegratedCircuitCardId = table.Column<string>(type: "text", nullable: true),
                    ValueAddedServices = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedUser = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileCountryCode");

            migrationBuilder.DropTable(
                name: "SimInformation");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
