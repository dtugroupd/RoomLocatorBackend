namespace Shared
{
    public static class ExceptionFactory
    {
        public static InvalidRequestException Create_FailedHttpRequest(string name)
        {
            return new InvalidRequestException("Failed to reach server", 
            $"Failed to access {name}. Their servers might be down, please try again later.");
        }
        
    }
}