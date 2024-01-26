namespace PicpayChallenge.Exceptions
{
    public class InvalidDocumentException : Exception
    {
        public InvalidDocumentException(string? message) : base(message) { }

        public static void Throw(string? message)
            => throw new InvalidDocumentException(message);
        public static void ThrowIfNull(object? obj)
        {
            if (obj is null)
                Throw($"{nameof(obj)} Invalid");
        }
    }
}
