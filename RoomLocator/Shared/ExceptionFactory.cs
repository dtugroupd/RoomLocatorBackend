namespace Shared
{
    /// <summary>
    /// For creating new exceptions with standard text
    /// <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public static class ExceptionFactory
    {
        public static InvalidRequestException Create_FailedHttpRequest(string name)
        {
            return new InvalidRequestException("Failed to reach server", 
                $"Failed to access {name}. Their servers might be down, please try again later.");
        }

        /// <summary>
        /// Used when credentials are incorrect or a user is refused access to the site
        /// </summary>
        /// <returns></returns>
        public static UnauthorizedException Unauthorized() => new UnauthorizedException();
    }
}
