using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserveStudent.Migrations
{
    public partial class upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "ReservationTypes",
                newName: "AccessNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ReservationTypes",
                newName: "NameType");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Reservations",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "NameType",
                table: "ReservationTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AccessNumber",
                table: "ReservationTypes",
                newName: "Number");
        }
    }
}
