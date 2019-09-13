namespace Shared
{
    public class InvalidRequestException : BaseException
    {
        public InvalidRequestException(string title, string message) : base(title, message) { }
    }
}