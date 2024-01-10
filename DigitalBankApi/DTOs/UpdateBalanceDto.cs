using DigitalBankApi.Enums;

namespace DigitalBankApi.DTOs
{
    public class UpdateBalanceDto
    {
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal Salary { get; set; }
    }
}
