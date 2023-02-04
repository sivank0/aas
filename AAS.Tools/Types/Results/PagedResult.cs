namespace AAS.Tools.Types.Results;

public static class PagedResult
{
    public static PagedResult<T> Create<T>(IEnumerable<T> values, long totalRows)
    {
        return new PagedResult<T>(values, totalRows);
    }
}

public class PagedResult<T>
{
    public List<T> Values { get; }
    public long TotalRows { get; }

    public PagedResult(List<T> values, long totalRows)
    {
        Values = values;
        TotalRows = totalRows;
    }

    public PagedResult(IEnumerable<T> values, long totalRows)
    {
        Values = new List<T>(values);
        TotalRows = totalRows;
    }
}
