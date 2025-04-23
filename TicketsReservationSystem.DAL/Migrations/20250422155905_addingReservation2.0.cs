using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addingReservation20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "avillableNumber",
                table: "Tickets",
                newName: "avillableCount");

            migrationBuilder.AddColumn<int>(
                name: "bookedCount",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookedCount",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "avillableCount",
                table: "Tickets",
                newName: "avillableNumber");
        }
    }
}
