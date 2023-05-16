#region

using System.Text.Json.Serialization;

#endregion

namespace AAS.Tools.Types.Results;

public class Result
{
    public Error[] Errors { get; }

    [JsonIgnore] public Boolean IsSuccess => Errors.Length == 0;

    public Result(List<Error> errors)
    {
        Errors = errors.ToArray();
    }

    [JsonConstructor]
    public Result(Error[] errors)
    {
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(new List<Error>());
    }

    public static Result Fail(Error error)
    {
        return new Result(new List<Error>() { error });
    }

    public static Result Fail(String error)
    {
        return new Result(new List<Error>() { new Error(error) });
    }
}