using System;

namespace Shared
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string title, string message) : base(title, message) { }
        
        public static NotFoundException NotExists<T>()
        {
            return NotFoundException.PrependMessage<T>($"No {typeof(T).Name} exists.", null);
        }

        public static NotFoundException NotExists<T>(string id, string errorMessage = null)
        {
            return NotFoundException.NotExistsWithProperty<T>("Id", id, errorMessage);
        }

        public static NotFoundException NotExistsWithProperty<T>(string property, string id, string errorMessage = null)
        {
            return NotFoundException.PrependMessage<T>(
                $"No {typeof(T).Name} with {property} '{id}' exists.",
                errorMessage
            );
        }

        private static NotFoundException PrependMessage<T>(string message, string preMessage)
        {
            if (!string.IsNullOrEmpty(preMessage))
            {
                message += $" Error: {preMessage}";
            }
            
            return new NotFoundException($"{typeof(T).Name} not found", message);
        }
    }
}