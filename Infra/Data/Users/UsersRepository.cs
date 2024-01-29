using Microsoft.EntityFrameworkCore;
using PicpayChallenge.Application.Services;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Exceptions;
using PicpayChallenge.Helpers;
using PicpayChallenge.Presentation.DTOs;

namespace PicpayChallenge.Infra.Data.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly PicpayDbContext _context;

        public UsersRepository(PicpayDbContext context)
        {
            _context = context;
        }
        public async Task AddUser(User user)
        {
            if (user == null)
                throw new UserDataException(nameof(user));

            if (IsDocAlreadyRegistered(user.CPF_CNPJ))
                throw new UserDataException("Document already registered.");

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        private bool IsDocAlreadyRegistered(string CPF_CNPJ)
        {
            bool isRegistered = _context.Users.Any(x => x.CPF_CNPJ == CPF_CNPJ);

            if (isRegistered)
                return true;
            return false;
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    throw new UserDataException(nameof(user));

                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                foreach (var user in users)
                {
                    await GetById(user.Id);
                }

                return users;
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<User> GetById(Guid id)
        {
            try
            {
                var user = await _context.Users
                     .Include(u => u.ToTransactions)
                     .Include(u => u.FromTransactions)
                     .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new UserDataException(nameof(user));

                return user;
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<User> GetByDocument(string doc)
        {
            if (string.IsNullOrEmpty(doc))
                throw new UserDataException(nameof(doc));
            try
            {
                string formattedDoc = FormatDocument(doc);

                var user = await _context.Users.FirstOrDefaultAsync(x => x.CPF_CNPJ == formattedDoc);

                if (user == null)
                    throw new UserDataException(nameof(user));

                await GetById(user.Id);

                return user;

            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task UpdateUser(User user)
        {
            if (user == null)
                throw new UserDataException(nameof(user));
            try
            {
                var userToUpdate = await GetById(user.Id);

                if (userToUpdate == null)
                    throw new UserDataException("User not found.");

                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;

                _context.Users.Update(userToUpdate);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        private static string FormatDocument(string doc)
        {
            return doc.Trim()
                .Replace(".", "")
                .Replace("-", "")
                .Replace("/", "");
        }


    }
}
