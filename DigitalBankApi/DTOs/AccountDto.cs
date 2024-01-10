using DigitalBankApi.Enums;

namespace DigitalBankApi.DTOs
{
    public class AccountDto
    {
        public int CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal Salary { get; set; }
    }
}
