using System.Text.Json.Serialization;

namespace AAS.Tools.Types.Results;

public class Result
{
    public Error[] Errors { get; }

    public bool IsSuccess => Errors.Length == 0;

    [JsonConstructor]
    public Result(List<Error> errors)
    {
        Errors = errors.ToArray();
    }

    public static Result Success()
    {
        return new Result(new List<Error>());
    }

    public static Result Fail(Error error)
    {

        return new Result(new List<Error>() { error });
    }

    public static Result Fail(string error)
    {

        return new Result(new List<Error>() { new Error(error) });
    }
}
