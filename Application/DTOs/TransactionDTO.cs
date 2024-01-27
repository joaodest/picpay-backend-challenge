using PicpayChallenge.Presentation.DTOs;

namespace PicpayChallenge.Application.DTOs
{
    public class TransactionDTO
    {
        public  Guid Id { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
