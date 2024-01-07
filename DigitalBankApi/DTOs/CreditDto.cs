namespace DigitalBankApi.DTOs
{
    public class CreditDto
    {
        public decimal TotalAmount { get; set; }
        public decimal MontlyPayment { get; set; }
        public int RepaymentPeriodMonths { get; set; }
    }
}
