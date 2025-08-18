using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCP_Portal_Events.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "EventTypes",
                newName: "EventTypeName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventTypeName",
                table: "EventTypes",
                newName: "Type");
        }
    }
}
