using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCP_Portal_Events.Migrations
{
    /// <inheritdoc />
    public partial class AddEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventSpecialityId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EventCreatedDate", "EventSpecialityId", "EventStatusId", "EventTypeId", "ImageUrl", "LinkToEvent", "NoOfAttendees", "Title" },
                values: new object[] { 6, "Exploring the latest advancements in cardiology treatments and technology", new DateTime(2026, 5, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1, "/images/events/AdobeStock_65704664-scaled.jpeg", "https://zoom.us/cardiology-innovations-2025", 150, "Cardiology Innovations 2025" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventSpecialityId",
                value: 3);
        }
    }
}
