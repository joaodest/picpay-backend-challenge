using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PicpayChallenge.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid FromUserId { get; set; }

        [JsonIgnore]
        public User FromUser { get; set; } 

        public Guid ToUserId { get; set; }
        [JsonIgnore]
        public User ToUser { get; set; }  

        public double Amount { get; set; }

    }
}