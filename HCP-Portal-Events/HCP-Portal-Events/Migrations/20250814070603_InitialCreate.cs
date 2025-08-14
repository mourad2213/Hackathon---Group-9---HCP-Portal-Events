using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCP_Portal_Events.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    noOfAttendees = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    imageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "CASE WHEN [Date] < GETDATE() THEN 'Previous' ELSE 'Upcoming' END", stored: true),
                    linkToevent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eventSpecialityId = table.Column<int>(type: "int", nullable: false),
                    EventStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventStatuses_EventStatusId",
                        column: x => x.EventStatusId,
                        principalTable: "EventStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Specialities_eventSpecialityId",
                        column: x => x.eventSpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    no = table.Column<int>(type: "int", nullable: false),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRegistrationToEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegistrationToEvents", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRegistrationToEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRegistrationToEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivitySpeakers",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySpeakers", x => new { x.ActivityId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ActivitySpeakers_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitySpeakers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ActivityTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Module" },
                    { 2, "Activity" }
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "CME" },
                    { 2, "Webinar" }
                });

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "Field" },
                values: new object[,]
                {
                    { 1, "Cardiology" },
                    { 2, "Neurology" },
                    { 3, "Pediatrics" },
                    { 4, "Oncology" },
                    { 5, "General Practice" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "EventStatusId", "EventTypeId", "Title", "eventSpecialityId", "imageUrl", "linkToevent", "noOfAttendees" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 13, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Continuing Medical Education for Cardiology", null, 1, "Cardiology CME 2023", 1, "https://example.com/events/cardio-cme.jpg", "https://zoom.us/cardio-cme-2023", 120 },
                    { 2, new DateTime(2025, 7, 30, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Latest updates in pediatric medicine", null, 1, "Pediatric CME Update", 3, "https://example.com/events/ped-cme.jpg", "", 80 },
                    { 3, new DateTime(2025, 8, 21, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Monthly webinars on neurology advancements", null, 2, "Neurology Webinar Series", 2, "https://example.com/events/neuro-webinar.jpg", "https://zoom.us/neuro-webinar", 75 },
                    { 4, new DateTime(2025, 9, 28, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Recent advances in cancer treatment", null, 2, "Oncology Webinar", 4, "https://example.com/events/onco-webinar.jpg", "https://zoom.us/onco-webinar", 90 },
                    { 5, new DateTime(2025, 8, 9, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Important updates for general practitioners", null, 2, "GP Webinar: Annual Updates", 5, "https://example.com/events/gp-webinar.jpg", "", 150 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PhoneNumber", "ProfilePicture", "SpecialityId", "UserName" },
                values: new object[,]
                {
                    { 1, "dr.smith@example.com", 1234567890L, "https://example.com/profiles/smith.jpg", 1, "dr_smith" },
                    { 2, "dr.jones@example.com", 2345678901L, "https://example.com/profiles/jones.jpg", 2, "dr_jones" },
                    { 3, "dr.williams@example.com", 3456789012L, "https://example.com/profiles/williams.jpg", 3, "dr_williams" },
                    { 4, "dr.brown@example.com", 4567890123L, "https://example.com/profiles/brown.jpg", 4, "dr_brown" },
                    { 5, "dr.taylor@example.com", 5678901234L, "https://example.com/profiles/taylor.jpg", 5, "dr_taylor" }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "ActivityTypeId", "Date", "Description", "EventId", "Title", "no" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 8, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Introduction to cardiology principles", 1, "Cardio Basics Module", 1 },
                    { 2, 2, new DateTime(2025, 8, 4, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Hands-on pediatric patient care", 2, "Pediatric Care Activity", 2 },
                    { 3, 1, new DateTime(2025, 8, 19, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642), "Fundamentals of neurology", 3, "Neuro Module 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserRegistrationToEvents",
                columns: new[] { "EventId", "UserId", "IsCancelled", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, 1, false, new DateTime(2025, 7, 26, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642) },
                    { 2, 1, true, new DateTime(2025, 7, 28, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642) },
                    { 2, 3, false, new DateTime(2025, 7, 27, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642) },
                    { 3, 2, false, new DateTime(2025, 7, 29, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642) },
                    { 4, 4, false, new DateTime(2025, 7, 30, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642) },
                    { 5, 3, true, new DateTime(2025, 8, 1, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642) },
                    { 5, 5, false, new DateTime(2025, 7, 31, 10, 6, 3, 114, DateTimeKind.Local).AddTicks(4642) }
                });

            migrationBuilder.InsertData(
                table: "ActivitySpeakers",
                columns: new[] { "ActivityId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 2 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Attachments",
                columns: new[] { "Id", "ActivityId", "FileName", "FilePath" },
                values: new object[,]
                {
                    { 1, 1, "cardio-module.pdf", "/files/cardio-module.pdf" },
                    { 2, 2, "pediatric-activity.pdf", "/files/pediatric-activity.pdf" },
                    { 3, 3, "neuro-module1.pdf", "/files/neuro-module1.pdf" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityTypeId",
                table: "Activities",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_EventId",
                table: "Activities",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySpeakers_UserId",
                table: "ActivitySpeakers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ActivityId",
                table: "Attachments",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_eventSpecialityId",
                table: "Events",
                column: "eventSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventStatusId",
                table: "Events",
                column: "EventStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegistrationToEvents_UserId",
                table: "UserRegistrationToEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpecialityId",
                table: "Users",
                column: "SpecialityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivitySpeakers");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "UserRegistrationToEvents");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventStatuses");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Specialities");
        }
    }
}
