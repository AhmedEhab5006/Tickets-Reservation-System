using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class schemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SportEvents",
                table: "SportEvents");

            migrationBuilder.DropIndex(
                name: "IX_SportEvents_EventId",
                table: "SportEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntertainmentEvents",
                table: "EntertainmentEvents");

            migrationBuilder.DropIndex(
                name: "IX_EntertainmentEvents_EventId",
                table: "EntertainmentEvents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SportEvents");

            migrationBuilder.DropColumn(
                name: "id",
                table: "EntertainmentEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportEvents",
                table: "SportEvents",
                column: "EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntertainmentEvents",
                table: "EntertainmentEvents",
                column: "EventId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65ed386e-30d2-4ef9-9eea-491fdc5824cd", "AQAAAAIAAYagAAAAEPhIMZxMPRQYv5tI+4p2Ybmo1gVRX1HYz/OKcMJZSptwxY3z0VSBdoxzl0POhmOcHg==", "e5b02788-a230-4706-9c11-8e24c928e4a4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SportEvents",
                table: "SportEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntertainmentEvents",
                table: "EntertainmentEvents");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SportEvents",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "EntertainmentEvents",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportEvents",
                table: "SportEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntertainmentEvents",
                table: "EntertainmentEvents",
                column: "id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14c60e1c-6f6d-4c6a-8a6c-623b9d2c9110",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19a6a2c2-45f9-4f22-9b59-c67960a85b91", "AQAAAAIAAYagAAAAEOk0dgE2pl0//pPVXqBRD6yfjgixvaH5xsp0GSWpCGMzgtLbhB9jumFyuCayP5M8Gw==", "e8ef2f5c-5659-4a6e-a221-b7ced049fb6a" });

            migrationBuilder.CreateIndex(
                name: "IX_SportEvents_EventId",
                table: "SportEvents",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentEvents_EventId",
                table: "EntertainmentEvents",
                column: "EventId",
                unique: true);
        }
    }
}
