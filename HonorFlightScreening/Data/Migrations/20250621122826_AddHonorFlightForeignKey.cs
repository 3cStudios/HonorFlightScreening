using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HonorFlightScreening.Migrations
{
    /// <inheritdoc />
    public partial class AddHonorFlightForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VeteranScreenings_HonorFlightId",
                table: "VeteranScreenings",
                column: "HonorFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_VeteranScreenings_HonorFlight_HonorFlightId",
                table: "VeteranScreenings",
                column: "HonorFlightId",
                principalTable: "HonorFlight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeteranScreenings_HonorFlight_HonorFlightId",
                table: "VeteranScreenings");

            migrationBuilder.DropIndex(
                name: "IX_VeteranScreenings_HonorFlightId",
                table: "VeteranScreenings");
        }
    }
}
