﻿// <auto-generated />
using System;
using DigitalBankApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DigitalBankApi.Migrations
{
    [DbContext(typeof(AdminDbContext))]
    [Migration("20240107101953_UserUpdate")]
    partial class UserUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DigitalBankApi.Helpers.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("UserId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DigitalBankApi.Models.AccountCredits", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("CreditId")
                        .HasColumnType("integer");

                    b.HasKey("AccountId", "CreditId");

                    b.HasIndex("CreditId");

                    b.ToTable("CustomerCredits");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Accounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("AccountID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountType")
                        .HasMaxLength(20)
                        .HasColumnType("integer");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BankScore")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("CustomerID");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalDailyTransferAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Credits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("CreditID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("MontlyPayment")
                        .HasColumnType("numeric");

                    b.Property<int>("RepaymentPeriodMonths")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("WithdrawalDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Customers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("CustomerID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DigitalBankApi.Models.DepositWithdraws", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("DepositWithdrawID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("AccountID");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TransactionType")
                        .HasMaxLength(10)
                        .HasColumnType("integer");

                    b.Property<bool>("isSucceded")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("DepositWithdraws");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Logins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("LoginID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("CustomerID");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("DigitalBankApi.Models.MoneyTransfers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("MoneyTransferID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("AccountID");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("DestAccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("MoneyTransfers");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Payees", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("PayeeID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PayeeType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PaymentDay")
                        .HasColumnType("integer");

                    b.Property<int>("accountId")
                        .HasColumnType("integer");

                    b.Property<bool>("isPayment")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("accountId");

                    b.ToTable("Payees");
                });

            modelBuilder.Entity("DigitalBankApi.Models.SupportRequests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("SupportRequestID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<DateTime?>("AnswerDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isAnswered")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("SupportRequests");
                });

            modelBuilder.Entity("DigitalBankApi.Models.AccountCredits", b =>
                {
                    b.HasOne("DigitalBankApi.Models.Accounts", "Account")
                        .WithMany("AccountCredits")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DigitalBankApi.Models.Credits", "Credit")
                        .WithMany("AccountCredits")
                        .HasForeignKey("CreditId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Credit");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Accounts", b =>
                {
                    b.HasOne("DigitalBankApi.Models.Customers", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DigitalBankApi.Models.DepositWithdraws", b =>
                {
                    b.HasOne("DigitalBankApi.Models.Accounts", "Account")
                        .WithMany("DepositWithdraws")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Logins", b =>
                {
                    b.HasOne("DigitalBankApi.Models.Customers", "Customer")
                        .WithMany("Logins")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DigitalBankApi.Models.MoneyTransfers", b =>
                {
                    b.HasOne("DigitalBankApi.Models.Accounts", "Account")
                        .WithMany("MoneyTransfers")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Payees", b =>
                {
                    b.HasOne("DigitalBankApi.Models.Accounts", "Account")
                        .WithMany("Payees")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DigitalBankApi.Models.SupportRequests", b =>
                {
                    b.HasOne("DigitalBankApi.Models.Customers", "Customer")
                        .WithMany("SupportRequests")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Accounts", b =>
                {
                    b.Navigation("AccountCredits");

                    b.Navigation("DepositWithdraws");

                    b.Navigation("MoneyTransfers");

                    b.Navigation("Payees");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Credits", b =>
                {
                    b.Navigation("AccountCredits");
                });

            modelBuilder.Entity("DigitalBankApi.Models.Customers", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Logins");

                    b.Navigation("SupportRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
