using MetaITAPI.Utils.Constants;

namespace MetaITAPI.Utils.Exceptions;

public class InvalidInputException : Exception
{
    public InvalidInputException() : base(StatusMessages.InvalidInputFields)
    {
    }

    public InvalidInputException(List<string> messages) : base(String.Join("; ", messages))
    {
    }

    public InvalidInputException(string message) : base(message)
    {
    }

    public InvalidInputException(string message, Exception inner) : base(message, inner)
    {
    }
}
