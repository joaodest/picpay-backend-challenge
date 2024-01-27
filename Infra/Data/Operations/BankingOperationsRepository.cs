
using PicpayChallenge.Exceptions;
using PicpayChallenge.Infra.Data.Users;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Domain.Entities;
using AutoMapper;
using PicpayChallenge.Application.DTOs;

namespace PicpayChallenge.Infra.Data.Operations
{
    public class BankingOperationsRepository : IBankingOperationsRepository
    {
        private readonly PicpayDbContext _context;

        private readonly IMapper _mapper;
        public BankingOperationsRepository(PicpayDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TransactionDTO GetTransactionDTO(Transaction tx)
        {
            var txDto = _mapper.Map<TransactionDTO>(tx);
            return txDto;
        }

        public async Task RevertTransfer(
            User fromUser,
            User toUser,
            Transaction reversalTx,
            double amount)
        {
            try
            {
                fromUser.Balance += amount;
                toUser.Balance -= amount;

                fromUser.ToTransactions.Remove(reversalTx);
                toUser.FromTransactions.Remove(reversalTx);

                _context.Users.Update(fromUser);
                _context.Users.Update(toUser);

                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"Error finding user due to {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Invalid argument due to {ex.Message}");
            }
        }

        public async Task Transfer(
            User fromUser,
            User toUser,
            Transaction tx,
            double amount)
        {
            try
            {
                fromUser.Balance -= amount;
                toUser.Balance += amount;

                fromUser.ToTransactions.Add(tx);
                toUser.FromTransactions.Add(tx);

                _context.Users.Update(fromUser);
                _context.Users.Update(toUser);

                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"Error finding user due to {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Invalid argument {ex.Message}");
            }
        }



    }
}
