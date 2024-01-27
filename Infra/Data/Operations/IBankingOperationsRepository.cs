using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.ValueObjects;

namespace PicpayChallenge.Infra.Data.Operations
{
    public interface IBankingOperationsRepository
    {
        Task RevertTransfer(User fromUser, User toUser, Transaction tx, double amount);
        Task Transfer(User fromUser, User toUser, Transaction tx, double amount);
    }
}
