using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_clientId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Clients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_clientId",
                table: "Tickets",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_clientId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

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
                name: "FK_Address_Clients_clientId",
                table: "Address",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_clientId",
                table: "Tickets",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
