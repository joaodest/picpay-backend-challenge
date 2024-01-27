using PicpayChallenge.Application.DTOs;
using PicpayChallenge.Domain.ValueObjects;

namespace PicpayChallenge.Presentation.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public double Amount { get; set; } = 0.0;
        public ICollection<TransactionDTO> SentTxs { get; set; } 
            = new List<TransactionDTO>()
            .OrderByDescending(t => t.CreatedAt)
            .ToList();
        public ICollection<TransactionDTO> ReceivedTxs { get; set; }
            = new List<TransactionDTO>()
            .OrderByDescending(t => t.CreatedAt)
            .ToList();
    }
}
