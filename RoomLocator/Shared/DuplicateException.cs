namespace Shared
{
    public class DuplicateException : BaseException
    {
        public DuplicateException(string message) : base("Duplicate entry", message) { }
        
        public static DuplicateException DuplicateEntry<T>() => new DuplicateException($"There already exist a similar {typeof(T).Name}"); 
    }
}