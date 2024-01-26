using Microsoft.AspNetCore.Identity;
using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PicpayChallenge.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public double Balance { get; set; } = 0.0;
        [Required]
        public string CPF_CNPJ { get; set; }
        public ICollection<Transaction> FromTransactions { get; set; } = new List<Transaction>();
        public ICollection<Transaction> ToTransactions { get; set; } = new List<Transaction>();

        public UserType UserType { get; set; }

    }
}
