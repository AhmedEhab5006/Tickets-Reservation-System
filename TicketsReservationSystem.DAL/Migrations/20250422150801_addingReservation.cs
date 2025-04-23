using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addingReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Address_shippingAddressId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_clientId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_clientId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_shippingAddressId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "seatNumber",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "shippingAddressId",
                table: "Tickets",
                newName: "avillableNumber");

            migrationBuilder.AddColumn<int>(
                name: "ClientUserId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    ticketId = table.Column<int>(type: "int", nullable: false),
                    shippingAddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservations_Address_shippingAddressId",
                        column: x => x.shippingAddressId,
                        principalTable: "Address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Tickets_ticketId",
                        column: x => x.ticketId,
                        principalTable: "Tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClientUserId",
                table: "Tickets",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_clientId",
                table: "Reservations",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_shippingAddressId",
                table: "Reservations",
                column: "shippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ticketId",
                table: "Reservations",
                column: "ticketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_ClientUserId",
                table: "Tickets",
                column: "ClientUserId",
                principalTable: "Clients",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_ClientUserId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ClientUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "avillableNumber",
                table: "Tickets",
                newName: "shippingAddressId");

            migrationBuilder.AddColumn<int>(
                name: "clientId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "seatNumber",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_clientId",
                table: "Tickets",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_shippingAddressId",
                table: "Tickets",
                column: "shippingAddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Address_shippingAddressId",
                table: "Tickets",
                column: "shippingAddressId",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_clientId",
                table: "Tickets",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
