using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class clientFinish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "totalPrice",
                table: "Reservations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "334d9a99-f362-49c4-8c56-6daa844ebd77", "AQAAAAIAAYagAAAAEIv4kaa0ZV3cXbLIdkL/frPN1OqYjxO2N10n+jr80buj6HmjYo+M1nTNgasCMPxWTg==", "76c05f3f-3a3e-4f17-b320-9713830d9a4c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65ed386e-30d2-4ef9-9eea-491fdc5824cd", "AQAAAAIAAYagAAAAEPhIMZxMPRQYv5tI+4p2Ybmo1gVRX1HYz/OKcMJZSptwxY3z0VSBdoxzl0POhmOcHg==", "e5b02788-a230-4706-9c11-8e24c928e4a4" });
        }
    }
}
