using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Infra.Data.Operations
{
    public interface IBankingOperationsRepository
    {
        Task RevertTransfer(User fromUser, User toUser, double amount);
        Task Transfer(User fromUser, User toUser, double amount);
    }
}
