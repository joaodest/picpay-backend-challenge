using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Exceptions;
using PicpayChallenge.Helpers;

namespace PicpayChallenge.Infra.Data
{
    public class UsersRepository : IUsersRepository
    {
        private List<User> _users;


        public UsersRepository(List<User> users)
        {
            _users = users;
            // Add users to the list
            _users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "johndoe@example.com",
                UserDoc = (UserDoc)"123456789",
                Password = "password123",
                Balance = 100.00,
                UserType = UserType.Normal
            }); ;

            _users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                UserDoc = (UserDoc)"987654321",
                Password = "password456",
                Balance = 200.00,
                UserType = UserType.Logista
            });
        }

        public Task AddUser(User user)
        {
            if (user == null)
                throw new UserDataException(nameof(user));

            _users.Add(user);
            return Task.CompletedTask;

        }

        public Task DeleteUser(Guid id)
        {
            try
            {
                var user = _users.FirstOrDefault(x => x.Id == id);

                if (user == null)
                    throw new UserDataException(nameof(user));

                _users.Remove(user);
                return Task.CompletedTask;

            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public Task<ICollection<User>> GetAll()
        {
            var users = _users;
            foreach (var user in users)
            {
                users.Add(user);
            }
            return Task.FromResult<ICollection<User>>(users);
        }

        public Task<User> GetById(Guid id)
        {
            try
            {
                var user = _users.FirstOrDefault(x => x.Id == id);

                if (user == null)
                    throw new UserDataException(nameof(user));

                return Task.FromResult(user);
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }
        }

        public Task UpdateUser(User user)
        {
            if (user == null)
                throw new UserDataException(nameof(user));
            try
            {
                var userToRemove = _users.FirstOrDefault(x => x.Id == user.Id);

                if (userToRemove == null)
                    throw new UserDataException(nameof(userToRemove));

                _users.Remove(userToRemove);
                _users.Add(user);

                return Task.FromResult(user);
            }
            catch (Exception e)
            {
                throw new UserDataException(e.Message);
            }

        }
    }
}
