using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCP_Portal_Events.Migrations
{
    /// <inheritdoc />
    public partial class EditUserImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProfilePicture",
                value: "//images/users/download.webp");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProfilePicture",
                value: "/images/users/download.webp");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProfilePicture",
                value: "/images/users/download.webp");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProfilePicture",
                value: "/images/users/download.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProfilePicture",
                value: "/images/users/OIP_1.jpeg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProfilePicture",
                value: "/images/users/OIP_2.webp");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProfilePicture",
                value: "/images/users/OIP_3.jpeg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProfilePicture",
                value: "/images/users/OIP_4.webp");
        }
    }
}
