
using PicpayChallenge.Exceptions;
using PicpayChallenge.Infra.Data.Users;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Infra.Data.Operations
{
    public class BankingOperationsRepository : IBankingOperationsRepository
    {
        private readonly PicpayDbContext _context;
        public BankingOperationsRepository(PicpayDbContext context)
        {
            _context = context;
        }

        public async Task RevertTransfer(
            User fromUser,
            User toUser,
            double amount)
        {
            try
            {
                fromUser.Balance += amount;
                toUser.Balance -= amount;

                var reversalTx = new Transaction(fromUser.Id, toUser.Id);
                fromUser.ToTransactions.Remove(reversalTx);
                toUser.FromTransactions.Remove(reversalTx);

                _context.Users.Update(fromUser);
                _context.Users.Update(toUser);

                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"Error finding user: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Invalid argument: {ex.Message}");
            }
        }

        public async Task Transfer(
            User fromUser,
            User toUser,
            double amount)
        {
            try
            {
                fromUser.Balance -= amount;
                toUser.Balance += amount;

                var transaction = new Transaction(fromUser.Id, toUser.Id);

                fromUser.ToTransactions.Add(transaction);
                toUser.FromTransactions.Add(transaction);

                _context.Users.Update(fromUser);
                _context.Users.Update(toUser);

                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"Error finding user: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Invalid argument: {ex.Message}");
            }
        }



    }
}
