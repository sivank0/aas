namespace AAS.Tools.Types.Results;

public class Error
{
    public string Message { get; }
    public string Key { get; }

    public Error(string message)
    {
        Message = message;
    }

    public Error(string key, string message)
    {
        Key = key;
        Message = message;
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(Key) ? Message : $"({Key}) {Message}";
    }
}