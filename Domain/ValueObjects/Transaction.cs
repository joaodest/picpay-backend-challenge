using PicpayChallenge.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PicpayChallenge.Domain.ValueObjects
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid FromUserId { get; set; }
        public User FromUser { get; set; }  // Propriedade de navegação
        public Guid ToUserId { get; set; }
        public User ToUser { get; set; }  // Propriedade de navegação

        public Transaction(Guid fromUserId, Guid toUserId)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            FromUserId = fromUserId;
            ToUserId = toUserId;
        }
    }
}