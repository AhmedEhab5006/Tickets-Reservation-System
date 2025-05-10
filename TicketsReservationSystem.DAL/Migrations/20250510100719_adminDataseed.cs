using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adminDataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8f8d4f6a-4e90-4a5c-92b2-f32483c6e7d1", null, "ApplicationUserRole", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "isDeleted" },
                values: new object[] { "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110", 0, "19a6a2c2-45f9-4f22-9b59-c67960a85b91", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEOk0dgE2pl0//pPVXqBRD6yfjgixvaH5xsp0GSWpCGMzgtLbhB9jumFyuCayP5M8Gw==", null, true, "e8ef2f5c-5659-4a6e-a221-b7ced049fb6a", false, "admin", false });
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object [] { "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110", "8f8d4f6a-4e90-4a5c-92b2-f32483c6e7d1" });
                

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f8d4f6a-4e90-4a5c-92b2-f32483c6e7d1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110");
        }
    }
}
