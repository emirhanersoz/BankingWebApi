using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DigitalBankApi.Migrations
{
    public partial class BankMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    CreditID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    MontlyPayment = table.Column<decimal>(type: "numeric", nullable: false),
                    RepaymentPeriodMonths = table.Column<int>(type: "integer", nullable: false),
                    WithdrawalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.CreditID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    City = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PostCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerID = table.Column<int>(type: "integer", nullable: false),
                    AccountType = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric", nullable: false),
                    BankScore = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDailyTransferAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerID = table.Column<int>(type: "integer", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginID);
                    table.ForeignKey(
                        name: "FK_Logins_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportRequests",
                columns: table => new
                {
                    SupportRequestID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerID = table.Column<int>(type: "integer", nullable: false),
                    Subject = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isAnswered = table.Column<bool>(type: "boolean", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    AnswerDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportRequests", x => x.SupportRequestID);
                    table.ForeignKey(
                        name: "FK_SupportRequests_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCredits",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    CreditId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCredits", x => new { x.AccountId, x.CreditId });
                    table.ForeignKey(
                        name: "FK_CustomerCredits_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_CustomerCredits_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "CreditID");
                });

            migrationBuilder.CreateTable(
                name: "DepositWithdraws",
                columns: table => new
                {
                    DepositWithdrawID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountID = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    isSucceded = table.Column<bool>(type: "boolean", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositWithdraws", x => x.DepositWithdrawID);
                    table.ForeignKey(
                        name: "FK_DepositWithdraws_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoneyTransfers",
                columns: table => new
                {
                    MoneyTransferID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountID = table.Column<int>(type: "integer", nullable: false),
                    DestAccountId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Comment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransfers", x => x.MoneyTransferID);
                    table.ForeignKey(
                        name: "FK_MoneyTransfers_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payees",
                columns: table => new
                {
                    PayeeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Period = table.Column<string>(type: "text", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isPayment = table.Column<bool>(type: "boolean", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payees", x => x.PayeeID);
                    table.ForeignKey(
                        name: "FK_Payees_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_CreditId",
                table: "CustomerCredits",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositWithdraws_AccountID",
                table: "DepositWithdraws",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransfers_AccountID",
                table: "MoneyTransfers",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Payees_AccountId",
                table: "Payees",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportRequests_CustomerID",
                table: "SupportRequests",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCredits");

            migrationBuilder.DropTable(
                name: "DepositWithdraws");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "MoneyTransfers");

            migrationBuilder.DropTable(
                name: "Payees");

            migrationBuilder.DropTable(
                name: "SupportRequests");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
