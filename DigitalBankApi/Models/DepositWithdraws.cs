using DigitalBankApi.Enums;

namespace DigitalBankApi.Models
{
    public partial class DepositWithdraws
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool isSucceded { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Accounts Account { get; set; }
    }
}
