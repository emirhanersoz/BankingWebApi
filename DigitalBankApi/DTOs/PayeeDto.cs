using DigitalBankApi.Enums;

namespace DigitalBankApi.DTOs
{
    public class PayeeDto
    {
        public int accountId { get; set; }
        public PayeeType PayeeType { get; set; }
        public decimal Amount { get; set; }
        public int PaymentDay { get; set; }
    }
}
