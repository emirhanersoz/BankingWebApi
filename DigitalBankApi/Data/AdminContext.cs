using DigitalBankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankApi.Data
{
    public partial class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        { }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Credits> Credits { get; set; }
        public virtual DbSet<AccountCredits> CustomerCredits { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<DepositWithdraws> DepositWithdraws { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<MoneyTransfers> MoneyTransfers { get; set; }
        public virtual DbSet<Payees> Payees { get; set; }
        public virtual DbSet<SupportRequests> SupportRequests { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountCredits>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.CreditId });

                entity.HasOne(e => e.Account)
                    .WithMany(p => p.AccountCredits)
                    .HasForeignKey(p => p.AccountId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Credit)
                    .WithMany(p => p.AccountCredits)
                    .HasForeignKey(p => p.CreditId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("AccountID")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.CustomerId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Balance)
                    .IsRequired();

                entity.Property(e => e.TotalDailyTransferAmount)
                    .IsRequired();

                entity.Property(e => e.CreatedDate)
                    .IsRequired();
            });

            modelBuilder.Entity<Credits>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("CreditID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TotalAmount)
                    .IsRequired();

                entity.Property(e => e.MontlyPayment)
                    .IsRequired();

                entity.Property(e => e.RepaymentPeriodMonths)
                    .IsRequired();

                entity.Property(e => e.WithdrawalDate)
                    .IsRequired();
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired();

                entity.Property(e => e.City)
                    .HasMaxLength(30);

                entity.Property(e => e.State)
                    .HasMaxLength(30);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<DepositWithdraws>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("DepositWithdrawID")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.AccountId);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.DepositWithdraws)
                    .HasForeignKey(d => d.AccountId);

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID");

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.isSucceded)
                    .IsRequired();

                entity.Property(e => e.TransactionDate)
                    .IsRequired();
            });

            modelBuilder.Entity<Logins>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("LoginID")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.UserId);

                entity.Property(e => e.LoginTime)
                    .IsRequired();
            });

            modelBuilder.Entity<MoneyTransfers>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("MoneyTransferID")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.AccountId);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.MoneyTransfers)
                    .HasForeignKey(d => d.AccountId);

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID");

                entity.Property(e => e.DestAccountId)
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.Property(e => e.Comment)
                    .HasMaxLength(100);

                entity.Property(e => e.TransactionDate)
                    .IsRequired();
            });

            modelBuilder.Entity<Payees>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("PayeeID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PayeeType)
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.Property(e => e.CreatedDate)
                    .IsRequired();

                entity.Property(e => e.PaymentDay)
                    .IsRequired();

                entity.Property(e => e.PaymentDate)
                    .IsRequired();

                entity.Property(e => e.isPayment)
                    .IsRequired();
            });

            modelBuilder.Entity<SupportRequests>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("SupportRequestID")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.CustomerId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RequestDate)
                    .IsRequired();

                entity.Property(e => e.isAnswered)
                    .IsRequired();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdentificationNumber);

                entity.Property(e => e.IdentificationNumber)
                    .HasColumnName("Identification Number");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
