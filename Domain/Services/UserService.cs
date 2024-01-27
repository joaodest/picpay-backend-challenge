using AutoMapper;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.Interfaces;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Exceptions;
using PicpayChallenge.Helpers;
using PicpayChallenge.Infra.Data.Users;
using PicpayChallenge.Presentation.DTOs;


namespace PicpayChallenge.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UserService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUser(string name,
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
                
                var userDto = GetUserDto(user.Id);

                return userDto;
            }
            catch (Exception e)
            {
                throw new UserDataException($"Unable to create user due to: {e.Message}");
            }

        }

        public UserDTO GetUserDto(Guid id)
        {
            User user = _usersRepository.GetById(id).Result;

            return _mapper.Map<UserDTO>(user);
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

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = await _usersRepository.GetAll();
                
                IEnumerable<UserDTO> dtoUsers = _mapper.Map<IEnumerable<UserDTO>>(users);

                return dtoUsers;
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            try
            {
                var user = await _usersRepository.GetById(id);
                var userDto = _mapper.Map<UserDTO>(user);

                return userDto;
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public async Task<UserDTO> GetUserByDocument(string document)
        {
            try
            {
                var user = await _usersRepository.GetByDocument(document);
                var userDto = _mapper.Map<UserDTO>(user);

                return userDto;
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

        public async Task<List<Transaction>> GetUserTransactions(string userDocument)
        {
            var user = await _usersRepository.GetByDocument(userDocument);

            var txList = user.FromTransactions;

            return (List<Transaction>)txList;
        }
    }
}
