﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HonorFlightScreening.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVeteranScreeningType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeteranScreenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeteranName = table.Column<string>(type: "nvarchar(250)", maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasPcpSignature = table.Column<bool>(type: "bit", nullable: true),
                    RequiresOxygen = table.Column<bool>(type: "bit", nullable: true),
                    RequiresInsulin = table.Column<bool>(type: "bit", nullable: true),
                    TakesMedications = table.Column<bool>(type: "bit", nullable: true),
                    RequiresAssistiveDevice = table.Column<bool>(type: "bit", nullable: true),
                    AssistiveDeviceType = table.Column<int>(type: "int", nullable: true),
                    HasMobilityLimitations = table.Column<bool>(type: "bit", nullable: true),
                    CanWalkLongDistances = table.Column<bool>(type: "bit", nullable: true),
                    RequiresWheelchairAccess = table.Column<bool>(type: "bit", nullable: true),
                    HasMedicalAlerts = table.Column<bool>(type: "bit", nullable: true),
                    HasMobilityAlerts = table.Column<bool>(type: "bit", nullable: true),
                    HasSpecialAlerts = table.Column<bool>(type: "bit", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
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
