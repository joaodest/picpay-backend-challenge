using PicpayChallenge.Application.DTOs;
using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Infra.Data.Operations
{
    public interface IBankingOperationsRepository
    {
        Task RevertTransfer(Transaction tx, double amount);
        Task Transfer(Transaction tx, double amount);

        Task AddTx(Transaction tx);

        Task<Transaction> GetTxById(Guid id);

        Task<Transaction> DeleteTx(Guid id);

        Task<ICollection<Transaction>> GetTxs();
        Task<ICollection<Transaction>> GetAllReceivedTxsByUserDocument(string document);
        Task<ICollection<Transaction>> GetAllSendedTxsByUserDocument(string document);
    }
}
