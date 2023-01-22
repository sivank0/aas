using System;
using System.Collections.Generic;
using System.Linq;

namespace PMS.Tools.Types.IDs
{
    public static class IDExtensions
    {
        public static ID ToID(this String idString)
        {
            return new ID(idString);
        }

        public static ID? TryToID(this String idString)
        {
            if (String.IsNullOrWhiteSpace(idString)) return null;

            return new ID(idString);
        }

        public static ID[] ToIDs(this IList<String> idStrings)
        {
            return idStrings.Select(x => x.ToID()).ToArray();
        }

        public static ID?[] TryToIDs(this String[] idStrings)
        {
            return Array.ConvertAll(idStrings, x => x.TryToID());
        }

        public static String TryToString(this ID id)
        {
            if (id == null) return String.Empty;

            return id.ToString();
        }

        public static String[] TryToStrings(this ID[] ids)
        {
            return Array.ConvertAll(ids, x => x.TryToString());
        }
    }
}
