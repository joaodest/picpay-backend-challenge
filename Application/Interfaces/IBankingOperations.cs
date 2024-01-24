using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Application.Interfaces
{
    public interface IBankingOperations
    {
        Task Transfer(float value, User user);
    }
}
