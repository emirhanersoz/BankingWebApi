using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBankApi.Migrations
{
    public partial class updateAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserID",
                table: "Customers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserID",
                table: "Customers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Identification Number",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Customers");
        }
    }
}
