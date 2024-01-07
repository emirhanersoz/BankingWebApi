namespace DigitalBankApi.DTOs
{
    public class MoneyTransferDto
    {
        public int AccountId { get; set; }
        public int DestAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
