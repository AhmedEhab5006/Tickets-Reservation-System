using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_ClientUserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Address_addressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_vendors_vendorId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Address_shippingAddressId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_clientId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tickets_ticketId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_ClientUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_vendors_Users_userId",
                table: "vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vendors",
                table: "vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "vendors",
                newName: "Vendors");

            migrationBuilder.RenameIndex(
                name: "IX_vendors_userId",
                table: "Vendors",
                newName: "IX_Vendors_userId");

            migrationBuilder.RenameColumn(
                name: "ClientUserId",
                table: "Tickets",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ClientUserId",
                table: "Tickets",
                newName: "IX_Tickets_ClientId");

            migrationBuilder.RenameColumn(
                name: "ticketId",
                table: "Reservations",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "shippingAddressId",
                table: "Reservations",
                newName: "ShippingAddressId");

            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "Reservations",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "bookedCount",
                table: "Reservations",
                newName: "BookedCount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reservations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ticketId",
                table: "Reservations",
                newName: "IX_Reservations_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_shippingAddressId",
                table: "Reservations",
                newName: "IX_Reservations_ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_clientId",
                table: "Reservations",
                newName: "IX_Reservations_ClientId");

            migrationBuilder.RenameColumn(
                name: "addressId",
                table: "Clients",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_addressId",
                table: "Clients",
                newName: "IX_Clients_AddressId");

            migrationBuilder.RenameColumn(
                name: "ClientUserId",
                table: "Address",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ClientUserId",
                table: "Address",
                newName: "IX_Address_ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_ClientId",
                table: "Address",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Address_AddressId",
                table: "Clients",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Vendors_vendorId",
                table: "Events",
                column: "vendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Address_ShippingAddressId",
                table: "Reservations",
                column: "ShippingAddressId",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_ClientId",
                table: "Reservations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tickets_TicketId",
                table: "Reservations",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Users_userId",
                table: "Vendors",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_ClientId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Address_AddressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Vendors_vendorId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Address_ShippingAddressId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_ClientId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tickets_TicketId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Users_userId",
                table: "Vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Vendors",
                newName: "vendors");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_userId",
                table: "vendors",
                newName: "IX_vendors_userId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Tickets",
                newName: "ClientUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ClientId",
                table: "Tickets",
                newName: "IX_Tickets_ClientUserId");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Reservations",
                newName: "ticketId");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressId",
                table: "Reservations",
                newName: "shippingAddressId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Reservations",
                newName: "clientId");

            migrationBuilder.RenameColumn(
                name: "BookedCount",
                table: "Reservations",
                newName: "bookedCount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_TicketId",
                table: "Reservations",
                newName: "IX_Reservations_ticketId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ShippingAddressId",
                table: "Reservations",
                newName: "IX_Reservations_shippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations",
                newName: "IX_Reservations_clientId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Clients",
                newName: "addressId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_AddressId",
                table: "Clients",
                newName: "IX_Clients_addressId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Address",
                newName: "ClientUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ClientId",
                table: "Address",
                newName: "IX_Address_ClientUserId");

            migrationBuilder.AlterColumn<int>(
                name: "addressId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_vendors",
                table: "vendors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_ClientUserId",
                table: "Address",
                column: "ClientUserId",
                principalTable: "Clients",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Address_addressId",
                table: "Clients",
                column: "addressId",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_vendors_vendorId",
                table: "Events",
                column: "vendorId",
                principalTable: "vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Address_shippingAddressId",
                table: "Reservations",
                column: "shippingAddressId",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_clientId",
                table: "Reservations",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tickets_ticketId",
                table: "Reservations",
                column: "ticketId",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_ClientUserId",
                table: "Tickets",
                column: "ClientUserId",
                principalTable: "Clients",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_Users_userId",
                table: "vendors",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
