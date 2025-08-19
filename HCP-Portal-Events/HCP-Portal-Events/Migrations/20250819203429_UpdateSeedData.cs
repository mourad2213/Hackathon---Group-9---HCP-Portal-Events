using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCP_Portal_Events.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActivityTypeId", "Date", "Description", "EventId", "Title" },
                values: new object[] { 1, new DateTime(2025, 1, 27, 9, 0, 0, 0, DateTimeKind.Unspecified), "In-depth cardiology practices", 1, "Advanced Cardio Module" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActivityTypeId", "Date", "Description", "EventId", "Title" },
                values: new object[] { 2, new DateTime(2024, 12, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), "Hands-on pediatric patient care", 2, "Pediatric Care Activity" });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "ActivityTypeId", "Date", "DayorModule_No", "Description", "EventId", "Title" },
                values: new object[,]
                {
                    { 4, 1, new DateTime(2024, 12, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, "Latest research findings in pediatrics", 2, "Pediatric Research Module" },
                    { 5, 1, new DateTime(2025, 1, 6, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, "Fundamentals of neurology", 3, "Neuro Module 1" },
                    { 6, 1, new DateTime(2025, 1, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, "Advanced neurology topics", 3, "Neuro Module 2" },
                    { 7, 1, new DateTime(2025, 2, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cancer biology and basics", 4, "Oncology Module 1" },
                    { 8, 1, new DateTime(2025, 2, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, "Therapeutic strategies in oncology", 4, "Oncology Module 2" },
                    { 9, 2, new DateTime(2024, 12, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, "Annual GP best practices", 5, "GP Update Activity 1" },
                    { 10, 2, new DateTime(2024, 12, 29, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, "Case studies for general practitioners", 5, "GP Update Activity 2" }
                });

            migrationBuilder.InsertData(
                table: "ActivitySpeakers",
                columns: new[] { "ActivityId", "UserId" },
                values: new object[,]
                {
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FileName", "FilePath" },
                values: new object[] { "cardio-module1.pdf", "/files/cardio-module1.pdf" });

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FileName", "FilePath" },
                values: new object[] { "cardio-module2.pdf", "/files/cardio-module2.pdf" });

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FileName", "FilePath" },
                values: new object[] { "pediatric-activity.pdf", "/files/pediatric-activity.pdf" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventCreatedDate",
                value: new DateTime(2025, 1, 31, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "ActivitySpeakers",
                columns: new[] { "ActivityId", "UserId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 5, 2 },
                    { 6, 4 },
                    { 7, 4 },
                    { 8, 5 },
                    { 9, 5 },
                    { 10, 3 }
                });

            migrationBuilder.InsertData(
                table: "Attachments",
                columns: new[] { "Id", "ActivityId", "FileName", "FilePath" },
                values: new object[,]
                {
                    { 4, 4, "pediatric-research.pdf", "/files/pediatric-research.pdf" },
                    { 5, 5, "neuro-module1.pdf", "/files/neuro-module1.pdf" },
                    { 6, 6, "neuro-module2.pdf", "/files/neuro-module2.pdf" },
                    { 7, 7, "oncology-module1.pdf", "/files/oncology-module1.pdf" },
                    { 8, 8, "oncology-module2.pdf", "/files/oncology-module2.pdf" },
                    { 9, 9, "gp-activity1.pdf", "/files/gp-activity1.pdf" },
                    { 10, 10, "gp-activity2.pdf", "/files/gp-activity2.pdf" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "ActivitySpeakers",
                keyColumns: new[] { "ActivityId", "UserId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActivityTypeId", "Date", "Description", "EventId", "Title" },
                values: new object[] { 2, new DateTime(2024, 12, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), "Hands-on pediatric patient care", 2, "Pediatric Care Activity" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActivityTypeId", "Date", "Description", "EventId", "Title" },
                values: new object[] { 1, new DateTime(2025, 1, 6, 9, 0, 0, 0, DateTimeKind.Unspecified), "Fundamentals of neurology", 3, "Neuro Module 1" });

            migrationBuilder.InsertData(
                table: "ActivitySpeakers",
                columns: new[] { "ActivityId", "UserId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 2 },
                    { 3, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FileName", "FilePath" },
                values: new object[] { "cardio-module.pdf", "/files/cardio-module.pdf" });

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FileName", "FilePath" },
                values: new object[] { "pediatric-activity.pdf", "/files/pediatric-activity.pdf" });

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FileName", "FilePath" },
                values: new object[] { "neuro-module1.pdf", "/files/neuro-module1.pdf" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventCreatedDate",
                value: new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
