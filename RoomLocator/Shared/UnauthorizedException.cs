namespace Shared
{
    /// <summary>
    /// <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class UnauthorizedException : BaseException
    {
        internal UnauthorizedException() : base("Failed to sign in", "The given credentials are invalid. Try again.") { }
    }
}
