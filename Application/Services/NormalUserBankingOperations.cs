using AutoMapper;
using Microsoft.AspNetCore.Components.Routing;
using PicpayChallenge.Application.DTOs;
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

            Transaction tx = new(fromUser.Id, toUser.Id, amount);

            try
            {
                tx = await RealizeTransfer(fromUser, toUser, tx, amount);

                var resp = await _httpClient.GetAsync($"https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc");

                if (!resp.IsSuccessStatusCode)
                {
                    await RevertTransfer(tx, amount);
                    var content = await resp.Content.ReadAsStringAsync();
                    if (!content.Contains("Autorizado"))
                        throw new UnauthorizedException(
                                        $"Unable to execute transaction due to {resp.Content.ReadAsStringAsync().Result}");
                }

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
        private async Task<Transaction> RealizeTransfer(User fromUser, User toUser, Transaction tx, double amount)
        {
            try
            {
                await _bankingOperations.Transfer(fromUser, toUser, tx, amount);

                return tx;
            }
            catch (Exception e)
            {
                throw new Exception($"Error on transfer {e.Message}");
            }
        }

        private async Task RevertTransfer(Transaction tx, double amount)
        {
            var fromUser = tx.FromUser;
            var toUser = tx.ToUser;
            try
            {
                await _bankingOperations.RevertTransfer(fromUser, toUser, tx, amount);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on transfer {e.Message}");
            }
        }

    }
}
