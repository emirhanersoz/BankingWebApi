using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBankApi.Migrations
{
    public partial class UpdatePayee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Payees");

            migrationBuilder.AddColumn<int>(
                name: "PayeeType",
                table: "Payees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayeeType",
                table: "Payees");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Payees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
