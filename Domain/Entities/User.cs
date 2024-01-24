using Microsoft.AspNetCore.Identity;
using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Helpers;

namespace PicpayChallenge.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public double Balance { get; set; } = 0;
        public required UserDoc UserDoc { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public UserType UserType { get; set; }

        private readonly IBankingOperations _bankingOperations;

        public User(IBankingOperations bankingOperations)
        {
            _bankingOperations = bankingOperations;
        }
        

        

    }
}
