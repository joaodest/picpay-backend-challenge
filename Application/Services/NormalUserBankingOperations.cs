using Microsoft.AspNetCore.Components.Routing;
using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Exceptions;
using PicpayChallenge.Helpers;
using PicpayChallenge.Infra.Data.Operations;
using PicpayChallenge.Infra.Data.Users;

namespace PicpayChallenge.Application.Services
{
    public class NormalUserBankingOperations : IBankingOperations
    {
        private readonly IUsersRepository _repository;
        private readonly IBankingOperationsRepository _bankingOperations;
        private readonly HttpClient _httpClient;

        public NormalUserBankingOperations(
            IBankingOperationsRepository bankingOperations,
            IUsersRepository repository,
            HttpClient httpClient
            )
        {
            _repository = repository;
            _httpClient = httpClient;
            _bankingOperations = bankingOperations;
        }

        public async Task<Transaction> Transfer(string fromUserDocument,
             string toUserDocument,
             double amount)
        {
            if (string.IsNullOrEmpty(fromUserDocument))
                throw new UserDataException($"Invalid sender document");
            var fromUser = await _repository.GetByDocument(fromUserDocument);

            if (string.IsNullOrEmpty(toUserDocument))
                throw new UserDataException($"Invalid receiver document");
            var toUser = await _repository.GetByDocument(toUserDocument);

            try
            {
                if (IsLogista(fromUser))
                    throw new UserDataException($"Logista user can't transfer");

                if (!IsValidAmount(fromUser, amount))
                    throw new UserDataException($"Insuficient amount");

                var transfer = await RealizeTransfer(fromUser, toUser, amount);

                var resp = await _httpClient.GetAsync($"https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc");

                if (!resp.IsSuccessStatusCode)
                {
                    await RevertTransfer(fromUser, toUser, amount);
                    var content = await resp.Content.ReadAsStringAsync();
                    if (!content.Contains("Autorizado"))
                        throw new UnauthorizedException(
                                        $"Unable to execute transaction due to {resp.Content.ReadAsStringAsync().Result}");
                }
                return transfer;
            }
            catch (Exception e) when (e is UserDataException || e is UnauthorizedException)
            {
                await RevertTransfer(fromUser, toUser, amount);
                throw;
            }
        }



        private static bool IsLogista(User fromUser)
        {
            if (fromUser.UserType == UserType.Logista)
            {
                return true;
            }
            return false;
        }

        private static bool IsValidAmount(User user, double amount)
        {
            if (user.Balance < amount)
            {
                return false;
            }
            return true;
        }
        private async Task<Transaction> RealizeTransfer(User fromUser, User toUser, double amount)
        {
            try
            {
                await _bankingOperations.Transfer(fromUser, toUser, amount);

                var transaction = new Transaction(fromUser.Id, toUser.Id);

                await AddSenderTransaction(transaction, fromUser);
                await AddReceiverTransaction(transaction, toUser);

                return transaction;
            }
            catch (Exception e)
            {
                throw new Exception($"Error on transfer {e.Message}");
            }
        }

        private async Task AddSenderTransaction(Transaction tx, User user)
        {
            user.ToTransactions.Add(tx);
            await _repository.UpdateUser(user);
        }
        private async Task AddReceiverTransaction(Transaction tx, User toUser)
        {
            toUser.FromTransactions.Add(tx);
            await _repository.UpdateUser(toUser);
        }

        private async Task RevertTransfer(User fromUser, User toUser, double amount)
        {
            try
            {
                await _bankingOperations.RevertTransfer(fromUser, toUser, amount);

                var tx = new Transaction(fromUser.Id, toUser.Id);

                await RemoveSenderTransaction(tx, fromUser);
                await RemoveReceiverTransaction(tx, toUser);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on transfer {e.Message}");
            }
        }

        private async Task RemoveSenderTransaction(Transaction tx, User user)
        {
            user.ToTransactions.Remove(tx);
            await _repository.UpdateUser(user);
        }
        private async Task RemoveReceiverTransaction(Transaction tx, User toUser)
        {
            toUser.FromTransactions.Remove(tx);
            await _repository.UpdateUser(toUser);
        }
    }
}
