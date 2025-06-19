using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HonorFlightScreening.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVeteranScreening4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuardianName",
                table: "VeteranScreenings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianPhone",
                table: "VeteranScreenings",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LiftRequired",
                table: "VeteranScreenings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilityAlertsDetails",
                table: "VeteranScreenings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecialAlertsDetails",
                table: "VeteranScreenings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuardianName",
                table: "VeteranScreenings");

            migrationBuilder.DropColumn(
                name: "GuardianPhone",
                table: "VeteranScreenings");

            migrationBuilder.DropColumn(
                name: "LiftRequired",
                table: "VeteranScreenings");

            migrationBuilder.DropColumn(
                name: "MobilityAlertsDetails",
                table: "VeteranScreenings");

            migrationBuilder.DropColumn(
                name: "SpecialAlertsDetails",
                table: "VeteranScreenings");
        }
    }
}
