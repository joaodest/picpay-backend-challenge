using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Presentation.DTOs;


namespace PicpayChallenge.Domain.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(string name,
            string email,
            string pwd,
            string document, 
            double initialAmount);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(Guid id);
        Task UpdateUser(Guid id, string name = null, string email = null, string pwd = null);
        Task DeleteUser(Guid id);
        Task<UserDTO> GetUserByDocument(string document);
        UserDTO GetUserDto(Guid id);
        Task<List<Transaction>> GetUserTransactions(string userDocument);
    }
}
