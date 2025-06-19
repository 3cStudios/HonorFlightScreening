using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HonorFlightScreening.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVeteranScreening : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VeteranScreenings");

            migrationBuilder.RenameColumn(
                name: "TakesMedications",
                table: "VeteranScreenings",
                newName: "UseOxygen");

            migrationBuilder.RenameColumn(
                name: "RequiresWheelchairAccess",
                table: "VeteranScreenings",
                newName: "UseInsulin");

            migrationBuilder.RenameColumn(
                name: "RequiresOxygen",
                table: "VeteranScreenings",
                newName: "MedicalConcerns");

            migrationBuilder.RenameColumn(
                name: "RequiresInsulin",
                table: "VeteranScreenings",
                newName: "HelpWithInsulin");

            migrationBuilder.RenameColumn(
                name: "CanWalkLongDistances",
                table: "VeteranScreenings",
                newName: "FluidPills");

            migrationBuilder.AddColumn<bool>(
                name: "ConcernsFlying",
                table: "VeteranScreenings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ConcernsWalkingStairsBus",
                table: "VeteranScreenings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowMuchOxygen",
                table: "VeteranScreenings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicalConcernsDetails",
                table: "VeteranScreenings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcernsFlying",
                table: "VeteranScreenings");

            migrationBuilder.DropColumn(
                name: "ConcernsWalkingStairsBus",
                table: "VeteranScreenings");

            migrationBuilder.DropColumn(
                name: "HowMuchOxygen",
                table: "VeteranScreenings");

            migrationBuilder.DropColumn(
                name: "MedicalConcernsDetails",
                table: "VeteranScreenings");

            migrationBuilder.RenameColumn(
                name: "UseOxygen",
                table: "VeteranScreenings",
                newName: "TakesMedications");

            migrationBuilder.RenameColumn(
                name: "UseInsulin",
                table: "VeteranScreenings",
                newName: "RequiresWheelchairAccess");

            migrationBuilder.RenameColumn(
                name: "MedicalConcerns",
                table: "VeteranScreenings",
                newName: "RequiresOxygen");

            migrationBuilder.RenameColumn(
                name: "HelpWithInsulin",
                table: "VeteranScreenings",
                newName: "RequiresInsulin");

            migrationBuilder.RenameColumn(
                name: "FluidPills",
                table: "VeteranScreenings",
                newName: "CanWalkLongDistances");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VeteranScreenings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
