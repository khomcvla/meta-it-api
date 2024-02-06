namespace MetaITAPI.Utils.Exceptions
{
    public class BookAlreadyExistsException : Exception
    {
        public BookAlreadyExistsException(List<string> messages) : base(String.Join("; ", messages))
        {
        }

        public BookAlreadyExistsException(string message) : base(message)
        {
        }

        public BookAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class BookNotExistException : Exception
    {
        public BookNotExistException()
        {
        }

        public BookNotExistException(List<string> messages) : base(String.Join("; ", messages))
        {
        }

        public BookNotExistException(string message) : base(message)
        {
        }

        public BookNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
