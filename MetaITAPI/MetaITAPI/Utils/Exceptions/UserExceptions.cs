namespace MetaITAPI.Utils.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(List<string> messages) : base(String.Join("; ", messages))
        {
        }

        public AlreadyExistException(string message) : base(message)
        {
        }

        public AlreadyExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class NotExistException : Exception
    {
        public NotExistException()
        {
        }

        public NotExistException(List<string> messages) : base(String.Join("; ", messages))
        {
        }

        public NotExistException(string message) : base(message)
        {
        }

        public NotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
