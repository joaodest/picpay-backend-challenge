using PicpayChallenge.Domain.Entities;
using System.Collections.Generic;

namespace PicpayChallenge.Infra.Data
{
    public interface IUsersRepository
    {
        Task AddUser(User user);
        Task<ICollection<User>> GetAll();
        Task<User> GetById(Guid id);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id); 

    }
}
