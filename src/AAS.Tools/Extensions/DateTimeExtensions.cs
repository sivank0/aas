namespace AAS.Tools.Extensions;

public static class DateTimeExtensions
{
    public static DateTime BeginOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 00, 00, 00);
    }

    public static DateTime EndOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
    }

    public static DateTime BeginOfDay(this DateOnly date)
    {
        return date.ToDateTime(TimeOnly.MinValue);
    }

    public static DateTime EndOfDay(this DateOnly date)
    {
        return date.ToDateTime(TimeOnly.MaxValue);
    }
}