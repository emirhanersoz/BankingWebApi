namespace DigitalBankApi.Models
{
    public partial class Credits
    {
        public Credits() 
        { 
            AccountCredits = new HashSet<AccountCredits>();
        }

        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal MontlyPayment {  get; set; }
        public int RepaymentPeriodMonths { get; set; }
        public DateTime WithdrawalDate { get; set; } = DateTime.Now.ToUniversalTime();

        public virtual ICollection<AccountCredits> AccountCredits { get; set; }
    }
}
