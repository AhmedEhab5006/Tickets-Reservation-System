using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "clientId",
                table: "Address",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "clientId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_clientId",
                table: "Address",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
