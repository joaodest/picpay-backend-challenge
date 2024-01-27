using PicpayChallenge.Domain.Entities;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PicpayChallenge.Infra.Data.Users
{
    public interface IUsersRepository
    {
        Task AddUser(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> GetByDocument(string doc);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id);

    }
}
