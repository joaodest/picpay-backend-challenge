using Microsoft.AspNetCore.Identity;
using PicpayChallenge.Application.DTOs;
using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public List<Transaction> FromTransactions { get; set; }  = new List<Transaction>();
        public List<Transaction> ToTransactions { get; set; } = new List<Transaction>();

        public UserType UserType { get; set; }

    }
}
