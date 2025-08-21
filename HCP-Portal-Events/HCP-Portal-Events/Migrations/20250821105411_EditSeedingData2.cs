using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCP_Portal_Events.Migrations
{
    /// <inheritdoc />
    public partial class EditSeedingData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventCreatedDate",
                value: new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventCreatedDate",
                value: new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventCreatedDate",
                value: new DateTime(2025, 10, 29, 9, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventCreatedDate",
                value: new DateTime(2025, 1, 31, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventCreatedDate",
                value: new DateTime(2024, 12, 17, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventCreatedDate",
                value: new DateTime(2025, 1, 8, 9, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
