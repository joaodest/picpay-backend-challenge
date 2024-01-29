using PicpayChallenge.Application.DTOs;
using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Application.Interfaces
{
    public interface IBankingOperations
    {
        Task<TransactionDTO> Transfer(string fromUserDocument, string toUserDocument, double amount);
        Task<Transaction> GetTransaction(Guid id);

        Task<ICollection<Transaction>> GetTransactions();
        Task<Transaction> CreateTransaction(Guid fromUserId, Guid toUserId, double amount);

        Task<Transaction> DeleteTransaction(Guid id);
    }
}
