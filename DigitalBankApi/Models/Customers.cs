using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalBankApi.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Accounts = new HashSet<Accounts>();
            SupportRequests = new HashSet<SupportRequests>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; } = new HashSet<Accounts>();
        public virtual ICollection<SupportRequests> SupportRequests { get; set; } = new HashSet<SupportRequests>();
    }
}
