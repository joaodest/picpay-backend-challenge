namespace PicpayChallenge.Exceptions
{
    public class TransactionDataException : Exception
    {
        public TransactionDataException(string? message) : base(message) { }

        public static void Throw(string? message)
            => throw new TransactionDataException(message);

        public static void ThrowIfNull(object? obj)
        {
            if (obj is null)
                Throw($"{nameof(obj)} Invalid");
        }
    }
}
