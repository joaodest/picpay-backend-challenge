using AutoMapper;
using Microsoft.AspNetCore.Components.Routing;
using PicpayChallenge.Application.DTOs;
using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Domain.Entities;
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
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public NormalUserBankingOperations(
            IBankingOperationsRepository bankingOperations,
            IUsersRepository repository,
            IMapper mapper,
            HttpClient httpClient
            )
        {
            _repository = repository;
            _bankingOperations = bankingOperations;
            _mapper = mapper;
            _httpClient = httpClient;

        }

        public async Task<Transaction> CreateTransaction(Guid fromUserId, Guid toUserId, double amount)
        {
            User fromUser = await _repository.GetById(fromUserId);
            User toUser = await _repository.GetById(toUserId);
            try
            {
                var tx = new Transaction
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    FromUserId = fromUserId,
                    FromUser = fromUser,
                    ToUserId = toUserId,
                    ToUser = toUser,
                    Amount = amount
                };

                await _bankingOperations.AddTx(tx);

                return tx;
            }
            catch (Exception e) when (e is UserDataException || e is TransactionDataException)
            {
                throw;
            }
        }

        public Task<ICollection<Transaction>> GetTransactions()
        {

            return _bankingOperations.GetTxs();
        }

        public async Task<Transaction> GetTransaction(Guid id)
        {
            var tx = await _bankingOperations.GetTxById(id);

            if (tx == null)
                throw new TransactionDataException($"Transaction not found");

            return tx;
        }


        public async Task<Transaction> DeleteTransaction(Guid id)
        {
            try
            {
                var tx = await _bankingOperations.GetTxById(id);
                await _bankingOperations.DeleteTx(id);

                return tx;
            }
            catch (Exception e)
            {
                throw new TransactionDataException($"Error when deleting transaction {e.Message}");
            }
        }

        public TransactionDTO GetTransactionDTO(Transaction tx)
        {
            var txDto = _mapper.Map<TransactionDTO>(tx);
            return txDto;
        }

        public async Task<TransactionDTO> Transfer(string fromUserDocument,
             string toUserDocument,
             double amount)
        {

            if (string.IsNullOrEmpty(fromUserDocument))
                throw new UserDataException($"Invalid sender document");

            var fromUser = await _repository.GetByDocument(fromUserDocument);

            if (string.IsNullOrEmpty(toUserDocument))
                throw new UserDataException($"Invalid receiver document");
            var toUser = await _repository.GetByDocument(toUserDocument);

            if (IsLogista(fromUser))
                throw new UserDataException($"Logista user can't transfer");

            if (!IsValidAmount(fromUser, amount))
                throw new UserDataException($"Insuficient amount");

            var resp = await _httpClient.GetAsync("https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc");
            var content = await resp.Content.ReadAsStringAsync();


            if (!resp.IsSuccessStatusCode || !content.Contains("Autorizado"))
            {
                throw new UnauthorizedException("Unauthorized transaction");
            }

            var tx = await CreateTransaction(fromUser.Id, toUser.Id, amount);
            try
            {
                tx = await RealizeTransfer(tx, amount);

                var txDto = GetTransactionDTO(tx);

                return txDto;
            }
            catch (Exception e) when (e is UserDataException || e is UnauthorizedException)
            {
                await RevertTransfer(tx, amount);
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
        private async Task<Transaction> RealizeTransfer(Transaction tx, double amount)
        {
            try
            {
                await _bankingOperations.Transfer(tx, amount);

                return tx;
            }
            catch (Exception e)
            {
                throw new Exception($"Error on transfer {e.Message}");
            }
        }

        private async Task RevertTransfer(Transaction tx, double amount)
        {

            try
            {
                await _bankingOperations.RevertTransfer(tx, amount);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on transfer {e.Message}");
            }
        }


    }
}
