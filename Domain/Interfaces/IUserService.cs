using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(string name,
            string email,
            string pwd,
            string document, 
            double initialAmount);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task UpdateUser(Guid id, string name = null, string email = null, string pwd = null);
        Task DeleteUser(Guid id);
        Task<User> GetUserByDocument(string document);
    }
}
