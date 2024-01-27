using PicpayChallenge.Application.DTOs;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.ValueObjects;


namespace PicpayChallenge.Application.Interfaces
{
    public interface IBankingOperations
    {
        Task<TransactionDTO> Transfer(string fromUserDocument, string toUserDocument, double amount);
    }
}
