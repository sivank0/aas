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
		return new(data);
	}

	public static DataResult<T?> Failed(IEnumerable<Error> errors)
	{
		return new(default, errors);
	}

	public static DataResult<T?> Failed(string error)
	{
		return new(default, new[] { new Error(error) });
	}
}
