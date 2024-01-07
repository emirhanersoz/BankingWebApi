namespace DigitalBankApi.Models
{
    public partial class SupportRequests
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now.ToUniversalTime();
        public bool isAnswered { get; set; } = false;
        public string? Answer { get; set; } = null;
        public DateTime? AnswerDate { get; set; } = null;

        public virtual Customers Customer { get; set; }
    }
}
