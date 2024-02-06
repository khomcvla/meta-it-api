using System.Text.Json;

namespace MetaITAPI.Common.Responses;

public class ErrorResponse
{
    public string Message { get; }
    public List<object> Errors { get; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public ErrorResponse(string message, List<Tuple<string, string>> errors)
    {
        Message = message;
        Errors = new List<object>(errors);
    }

    public ErrorResponse(string message, List<Tuple<string, string, string>> errors)
    {
        Message = message;
        Errors = new List<object>(errors);
    }

    public ErrorResponse(string message, List<int> errors)
    {
        Message = message;
        Errors = errors.ConvertAll(x => (object)x).ToList();
    }

    public ErrorResponse(string message, List<string> errors)
    {
        Message = message;
        Errors = new List<object>(errors);
    }

    public ErrorResponse(string message, params string[] errors)
    {
        Message = message;
        Errors = new List<object>(errors.ToList());
    }
}
