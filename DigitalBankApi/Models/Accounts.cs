using DigitalBankApi.Enums;

namespace DigitalBankApi.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            AccountCredits = new HashSet<AccountCredits>();
            DepositWithdraws = new HashSet<DepositWithdraws>();
            MoneyTransfers = new HashSet<MoneyTransfers>();
            Payees = new HashSet<Payees>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal Salary { get; set; } = 0;
        public decimal BankScore { get; set; } = 0;
        public decimal TotalDailyTransferAmount { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();

        public virtual Customers Customer { get; set; }

        public virtual ICollection<AccountCredits> AccountCredits { get; set; }
        public virtual ICollection<DepositWithdraws> DepositWithdraws { get; set; }
        public virtual ICollection<MoneyTransfers> MoneyTransfers { get; set; }
        public virtual ICollection<Payees> Payees { get; set; }
    }
}
