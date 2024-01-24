using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Infra.Data;

namespace PicpayChallenge.Application.Services
{
    public class BankingOperations : IBankingOperations
    {
        public float _balance;
        private readonly IUsersRepository _repository;
        public BankingOperations(float balance, IUsersRepository repository)
        {
            _balance = balance;
            _repository = repository;
        }
        public async Task Transfer(float value, User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));  

                if(user.UserType == Helpers.UserType.Normal)
                {

                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
