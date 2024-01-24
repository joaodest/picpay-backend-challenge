namespace PicpayChallenge.Exceptions
{
    public class UserDataException : Exception
    {
        public UserDataException(string? message) : base(message) { }

        public static void Throw(string? message)
            => throw new UserDataException(message);

        public static void ThrowIfNull(object? obj)
        {
            if (obj is null)
                Throw($"{nameof(obj)} Not Found");
        }
    }

}
