
using PicpayChallenge.Exceptions;
using PicpayChallenge.Infra.Data.Users;
using PicpayChallenge.Domain.Entities;
using AutoMapper;
using PicpayChallenge.Application.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddTx(Transaction tx)
        {
            if (tx == null)
                throw new TransactionDataException($"Transaction is null");

            var fromUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == tx.FromUserId);
            var ToUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == tx.ToUserId);

            if (fromUser == null || ToUser == null)
                throw new UserDataException($"Users not found");

            tx.FromUser = fromUser;
            tx.ToUser = ToUser;

            try
            {
                _context.Transactions.Add(tx);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new TransactionDataException($"Erro when adding tx {tx} to database due to {e.Message}");
            }
        }

        public async Task<Transaction> GetTxById(Guid id)
        {
            var tx = await _context.Transactions.FindAsync(id);

            if (tx == null)
                throw new TransactionDataException($"Transaction not found");

            return tx;
        }

        public async Task<ICollection<Transaction>> GetAllSendedTxsByUserDocument(string document)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.CPF_CNPJ == document);

            if (user == null)
                throw new UserDataException($"User not found");

            var txs = user.FromTransactions.ToList();

            return txs;
        }

        public async Task<ICollection<Transaction>> GetAllReceivedTxsByUserDocument(string document)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.CPF_CNPJ == document);

            if (user == null)
                throw new UserDataException($"User not found");

            var txs = user.ToTransactions.ToList();

            return txs;
        }

        public async Task<ICollection<Transaction>> GetTxs()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> DeleteTx(Guid id)
        {
            var tx = await _context.Transactions.FindAsync(id);

            if (tx == null)
                throw new TransactionDataException($"Transaction not found");

            _context.Transactions.Remove(tx);
            await _context.SaveChangesAsync();

            return tx;
        }

        public TransactionDTO GetTransactionDTO(Transaction tx)
        {
            var txDto = _mapper.Map<TransactionDTO>(tx);
            return txDto;
        }
        public async Task Transfer(Transaction tx, double amount)
        {
            var fromUser = await _context.Users.FindAsync(tx.FromUserId);
            var toUser = await _context.Users.FindAsync(tx.ToUserId);

            if (fromUser == null || toUser == null)
                throw new UserDataException($"Users not found");

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                fromUser.Balance -= amount;
                toUser.Balance += amount;

                tx.FromUserId = fromUser.Id;
                tx.ToUserId = toUser.Id;

                fromUser.FromTransactions.Add(tx);
                toUser.ToTransactions.Add(tx);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error transferring funds: {ex.Message}");
            }

        }

        public async Task RevertTransfer(Transaction tx, double amount)
        {
            var fromUser = await _context.Users.FindAsync(tx.FromUserId);

            if (fromUser == null)
                throw new UserDataException($"Sender user not found");

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                fromUser.Balance += amount;

                _context.Users.Update(fromUser);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error reverting funds: {ex.Message}");
            }
        }

    }
}



