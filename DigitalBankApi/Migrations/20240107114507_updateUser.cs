using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBankApi.Migrations
{
    public partial class updateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Customers_CustomerID",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Logins");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Logins",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Users_UserId",
                table: "Logins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Identification Number",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Users_UserId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_UserId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Logins");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Logins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Customers_CustomerID",
                table: "Logins",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
