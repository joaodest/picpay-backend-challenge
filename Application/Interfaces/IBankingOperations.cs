using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.ValueObjects;


namespace PicpayChallenge.Application.Interfaces
{
    public interface IBankingOperations
    {
        Task<Transaction> Transfer(string fromUserDocument, string toUserDocument, double amount);
    }
}
