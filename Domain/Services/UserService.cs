using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.Interfaces;
using PicpayChallenge.Exceptions;
using PicpayChallenge.Helpers;
using PicpayChallenge.Infra.Data.Users;

namespace PicpayChallenge.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> CreateUser(string name,
            string email,
            string pwd,
            string document,
            double initialAmount)
        {
            try
            {
                HandleDocuments handleDocuments = new(document);
                string formattedDocument = handleDocuments.GetDocument();

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Password = pwd,
                    CPF_CNPJ = formattedDocument,
                    Balance = initialAmount,

                    UserType = formattedDocument.Length == 11
                    ? UserType.Normal
                    : UserType.Logista
                };

                await _usersRepository.AddUser(user);
                return user;
            }
            catch (Exception e)
            {
                throw new UserDataException($"Unable to create user due to: {e.Message}");
            }

        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                await _usersRepository.DeleteUser(id);
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = await _usersRepository.GetAll();
                return users;
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var user = await _usersRepository.GetById(id);
                return user;
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public Task<User> GetUserByDocument(string document)
        {
            try
            {
                var user = _usersRepository.GetByDocument(document);
                return user;
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }
        public async Task UpdateUser(Guid id, string newName = null, string newEmail = null, string newPassword = null)
        {
            var userToUpdate = await _usersRepository.GetById(id);
            if (userToUpdate == null)
                throw new UserDataException("User not found.");

            if (!string.IsNullOrEmpty(newName))
                userToUpdate.Name = newName;

            if (!string.IsNullOrEmpty(newEmail))
                userToUpdate.Email = newEmail;

            if (!string.IsNullOrEmpty(newPassword))
                userToUpdate.Password = newPassword;

            await _usersRepository.UpdateUser(userToUpdate);
        }
    }
}
