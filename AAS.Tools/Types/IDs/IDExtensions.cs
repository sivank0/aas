namespace AAS.Tools.Types.IDs;

public static class IDExtensions
{
    public static ID ToID(this string idString)
    {
        return new ID(idString);
    }

    public static ID? TryToID(this string idString)
    {
        if (string.IsNullOrWhiteSpace(idString)) return null;

        return new ID(idString);
    }

    public static ID[] ToIDs(this IList<string> idStrings)
    {
        return idStrings.Select(x => x.ToID()).ToArray();
    }

    public static ID?[] TryToIDs(this string[] idStrings)
    {
        return Array.ConvertAll(idStrings, x => x.TryToID());
    }

    public static string TryToString(this ID id)
    {
        if (id == null) return string.Empty;

        return id.ToString();
    }

    public static string[] TryToStrings(this ID[] ids)
    {
        return Array.ConvertAll(ids, x => x.TryToString());
    }
}