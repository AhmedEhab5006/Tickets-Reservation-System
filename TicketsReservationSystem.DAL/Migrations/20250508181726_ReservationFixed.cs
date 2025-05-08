using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ReservationFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_clientId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_clientId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "clientId1",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "clientId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_clientId",
                table: "Reservations",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_clientId",
                table: "Reservations",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_clientId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_clientId",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "clientId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "clientId1",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_clientId1",
                table: "Reservations",
                column: "clientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_clientId1",
                table: "Reservations",
                column: "clientId1",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
