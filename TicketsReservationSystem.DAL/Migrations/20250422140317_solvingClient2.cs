using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class solvingClient2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_clientId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "ClientUserId",
                table: "Address",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_ClientUserId",
                table: "Address",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_ClientUserId",
                table: "Address",
                column: "ClientUserId",
                principalTable: "Clients",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_ClientUserId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ClientUserId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "clientId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Address_clientId",
                table: "Address",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
