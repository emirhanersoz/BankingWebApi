using DigitalBankApi.Enums;

namespace DigitalBankApi.DTOs
{
    public class DepositWithdrawDto
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool isSucceded { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
