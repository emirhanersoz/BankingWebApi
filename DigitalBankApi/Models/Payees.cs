using DigitalBankApi.Enums;

namespace DigitalBankApi.Models
{
    public partial class Payees
    {
        public int Id { get; set; }
        public int accountId { get; set; }
        public PayeeType PayeeType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();
        public int PaymentDay { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool isPayment {  get; set; } = false;

        public virtual Accounts Account { get; set; }
    }
}
