using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBankApi.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payees_Accounts_AccountId",
                table: "Payees");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Payees",
                newName: "accountId");

            migrationBuilder.RenameIndex(
                name: "IX_Payees_AccountId",
                table: "Payees",
                newName: "IX_Payees_accountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payees_Accounts_accountId",
                table: "Payees",
                column: "accountId",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payees_Accounts_accountId",
                table: "Payees");

            migrationBuilder.RenameColumn(
                name: "accountId",
                table: "Payees",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Payees_accountId",
                table: "Payees",
                newName: "IX_Payees_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payees_Accounts_AccountId",
                table: "Payees",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
