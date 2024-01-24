
namespace PicpayChallenge.Domain.ValueObjects
{
    public class UserDoc
    {
        private readonly string _doc;
        public UserDoc(string doc)
        {
            _doc = doc.Trim()
                .Replace(".", "")
                .Replace("-", "");
        }

        public static explicit operator UserDoc(string v)
        {
            return new UserDoc(v);
        }
    }
}
