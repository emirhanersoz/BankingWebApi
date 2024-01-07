using DigitalBankApi.Enums;

namespace DigitalBankApi.Models
{
    public class Users
    {
        public Users()
        {
            Customers = new HashSet<Customers>();
            Logins = new HashSet<Logins>();
        }

        public string IdentificationNumber { get; set; } = string.Empty;
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime TokenExpires { get; set; } = DateTime.Now.ToUniversalTime();

        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Logins> Logins { get; set; } = new HashSet<Logins>();
    }
}
