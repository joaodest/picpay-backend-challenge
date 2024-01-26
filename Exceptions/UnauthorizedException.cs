namespace PicpayChallenge.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string? message) : base(message) { }

        public static void Throw(string? message)
            => throw new UnauthorizedException(message);

        public static void ThrowIfNull(object? obj)
        {
            if (obj is null)
                Throw($"{nameof(obj)} Invalid");
        }
    }
}
