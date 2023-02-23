using System.Text.Json.Serialization;

namespace PMS.Tools.Types.Pages;

public sealed class Page
{
    public static readonly Page Empty = new();
    public static Page<T> Create<T>(T[] values, Int64 totalRows) => new(values, totalRows);

    private Page() { }
}

public readonly struct Page<T>
{
    public static readonly Page<T> Empty;

    public T[] Values { get; }
    public readonly Int64 TotalRows { get; }

    public Int32 Count => Values.Length;
    public Boolean IsEmpty => Values.Length == 0;

    [JsonConstructor]
    public Page(T[] values, Int64 totalRows)
    {
        Values = values;
        TotalRows = totalRows;
    }

    public Page<R> Convert<R>(Func<T, R> converter)
    {
        if (IsEmpty) return Page<R>.Empty;

        return new(Values.Select(converter).ToArray(), TotalRows);
    }

    public override String ToString() => Count + "/" + TotalRows;

    public static implicit operator Page<T>(Page _) => Empty;
}