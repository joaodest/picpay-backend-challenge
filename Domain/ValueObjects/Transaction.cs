using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Domain.ValueObjects
{
    public class Transaction
    {
        public Guid TxId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User FromUser { get; set; }
        public User ToUser { get; set; }

        public Transaction(User fromUser, User toUser)
        {
            TxId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            FromUser = fromUser;
            ToUser = toUser;

            fromUser.Transactions.Add(this);
            toUser.Transactions.Add(this);
        }

    }
}
