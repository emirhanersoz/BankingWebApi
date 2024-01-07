using DigitalBankApi.Models;

namespace DigitalBankApi.DTOs
{
    public class LoginDto
    {
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
