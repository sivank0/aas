namespace AAS.Tools.Types.Results;

public readonly struct DataResult<T>
{
    public T? Data { get; }
    public Error[] Errors { get; }
    public bool IsSuccess => !Errors?.Any() ?? true;

    public DataResult(T data, IEnumerable<Error>? errors = null)
    {
        Data = data;
        Errors = errors?.ToArray() ?? new Error[0];
    }

    public static DataResult<T> Success(T data)
    {
        return new DataResult<T>(data);
    }

    public static DataResult<T?> Fail(IEnumerable<Error> errors)
    {
        return new DataResult<T?>(default, errors);
    }

    public static DataResult<T?> Fail(string error)
    {
        return new DataResult<T?>(default, new[] { new Error(error) });
    }
}