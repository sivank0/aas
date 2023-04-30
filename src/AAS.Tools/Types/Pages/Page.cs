#region

using System.Text.Json.Serialization;

#endregion

namespace PMS.Tools.Types.Pages;

public sealed class Page
{
    public static readonly Page Empty = new();

    public static Page<T> Create<T>(T[] values, long totalRows)
    {
        return new Page<T>(values, totalRows);
    }

    private Page()
    {
    }
}

public readonly struct Page<T>
{
    public static readonly Page<T> Empty;

    public T[] Values { get; }
    public readonly long TotalRows { get; }

    public int Count => Values.Length;
    public bool IsEmpty => Values.Length == 0;

    [JsonConstructor]
    public Page(T[] values, long totalRows)
    {
        Values = values;
        TotalRows = totalRows;
    }

    public Page<R> Convert<R>(Func<T, R> converter)
    {
        if (IsEmpty) return Page<R>.Empty;

        return new Page<R>(Values.Select(converter).ToArray(), TotalRows);
    }

    public override string ToString()
    {
        return Count + "/" + TotalRows;
    }

    public static implicit operator Page<T>(Page _)
    {
        return Empty;
    }
}