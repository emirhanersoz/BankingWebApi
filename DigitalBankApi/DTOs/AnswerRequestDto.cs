namespace DigitalBankApi.DTOs
{
    public class AnswerRequestDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        public bool isAnswered { get; set; } = false;
        public string? Answer { get; set; } = null;
        public DateTime? AnswerDate { get; set; } = null;
    }
}
