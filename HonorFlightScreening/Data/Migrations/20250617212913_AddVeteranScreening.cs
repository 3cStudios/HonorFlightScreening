using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HonorFlightScreening.Migrations
{
    /// <inheritdoc />
    public partial class AddVeteranScreening : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeteranScreenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VeteranName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HasPcpSignature = table.Column<bool>(type: "INTEGER", nullable: true),
                    RequiresOxygen = table.Column<bool>(type: "INTEGER", nullable: true),
                    RequiresInsulin = table.Column<bool>(type: "INTEGER", nullable: true),
                    TakesMedications = table.Column<bool>(type: "INTEGER", nullable: true),
                    RequiresAssistiveDevice = table.Column<bool>(type: "INTEGER", nullable: true),
                    AssistiveDeviceType = table.Column<int>(type: "INTEGER", nullable: true),
                    HasMobilityLimitations = table.Column<bool>(type: "INTEGER", nullable: true),
                    CanWalkLongDistances = table.Column<bool>(type: "INTEGER", nullable: true),
                    RequiresWheelchairAccess = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasMedicalAlerts = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasMobilityAlerts = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasSpecialAlerts = table.Column<bool>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeteranScreenings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeteranScreenings");
        }
    }
}
