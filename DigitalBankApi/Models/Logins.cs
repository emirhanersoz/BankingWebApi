namespace DigitalBankApi.Models
{
    public partial class Logins
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; } = DateTime.Now.ToLocalTime();

        public virtual Users User { get; set; }
    }
}
