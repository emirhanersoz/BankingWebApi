namespace DigitalBankApi.Models
{
    public partial class MoneyTransfers
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int DestAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now.ToUniversalTime();

        public virtual Accounts Account { get; set; }
    }
}
