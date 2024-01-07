using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBankApi.Migrations
{
    public partial class UpdatePaymentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Payees");

            migrationBuilder.AddColumn<int>(
                name: "PaymentDay",
                table: "Payees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDay",
                table: "Payees");

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Payees",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
