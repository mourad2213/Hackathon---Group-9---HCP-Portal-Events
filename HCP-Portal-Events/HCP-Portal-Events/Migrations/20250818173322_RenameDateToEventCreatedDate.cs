using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCP_Portal_Events.Migrations
{
    /// <inheritdoc />
    public partial class RenameDateToEventCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                 name: "Date",
                 table: "Events",
                 newName: "EventCreatedDate");
        
            migrationBuilder.RenameColumn(
                 name: "Type",
                 table: "Events",
                 newName: "EventTypeName");
         
            migrationBuilder.RenameColumn(
                name: "Status",
                 table: "Events",
                 newName: "EventStatusName");
         }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                 name: "Date",
                 table: "Events",
                 newName: "EventCreatedDate");

            migrationBuilder.RenameColumn(
                 name: "Type",
                 table: "Events",
                 newName: "EventTypeName");

            migrationBuilder.RenameColumn(
                name: "Status",
                 table: "Events",
                 newName: "EventStatusName");
        }
    }
}
