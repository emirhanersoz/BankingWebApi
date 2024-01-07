namespace DigitalBankApi.Models
{
    public partial class AccountCredits
    {
        public int AccountId { get; set; }
        public int CreditId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Credits Credit { get; set; }
    }
}
