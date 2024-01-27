using Microsoft.EntityFrameworkCore;
using PicpayChallenge.Application.Services;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Exceptions;
using PicpayChallenge.Helpers;

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
            try
            {
                if (user == null)
                    throw new UserDataException(nameof(user));

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
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
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetById(Guid id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);

                if (user == null)
                    throw new UserDataException(nameof(user));

                return await Task.FromResult(user);
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<User> GetByDocument(string doc)
        {
            try
            {
                if (string.IsNullOrEmpty(doc))
                    throw new UserDataException(nameof(doc));

                string formattedDoc = FormatDocument(doc);

                var user = _context.Users
                    .FirstOrDefault(x => x.CPF_CNPJ == formattedDoc);

                if (user == null)
                    throw new UserDataException(nameof(user));

                return await Task.FromResult(user);

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
                var userToUpdate = await _context.Users.FindAsync(user.Id);

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
