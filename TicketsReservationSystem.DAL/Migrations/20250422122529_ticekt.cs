using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ticekt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Tickets_ticketid",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ticketid",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ticketid",
                table: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ticketid",
                table: "Address",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_ticketid",
                table: "Address",
                column: "ticketid");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Tickets_ticketid",
                table: "Address",
                column: "ticketid",
                principalTable: "Tickets",
                principalColumn: "id");
        }
    }
}
