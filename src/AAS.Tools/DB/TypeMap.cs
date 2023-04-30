#region

using System.Data;
using AAS.Tools.Types;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Tools.DB;

internal static class TypeMap
{
    public static readonly Dictionary<Type, DbType> SqlTypeMap = new()
    {
        { typeof(string), DbType.String },
        { typeof(string[]), DbType.Object },
        { typeof(long), DbType.Int64 },
        { typeof(long[]), DbType.Object },
        { typeof(int), DbType.Int32 },
        { typeof(int[]), DbType.Object },
        { typeof(byte), DbType.Byte },
        { typeof(decimal), DbType.Decimal },
        { typeof(decimal[]), DbType.Object },
        { typeof(DateTime), DbType.DateTime },
        { typeof(DateOnly), DbType.Date },
        { typeof(DateOnly[]), DbType.Object },
        { typeof(DateTime[]), DbType.Object },
        { typeof(Guid), DbType.Guid },
        { typeof(Guid[]), DbType.Object },
        { typeof(ID), DbType.Binary },
        { typeof(ID[]), DbType.Object },
        { typeof(bool), DbType.Boolean },
        { typeof(bool[]), DbType.Object },
        { typeof(TimeSpan), DbType.Object },
        { typeof(TimeSpan[]), DbType.Object },
        { typeof(double), DbType.Double },
        { typeof(double[]), DbType.Object },
        { typeof(Jsonb), DbType.Object }
    };

    public static DbType LookupDbType(Type type)
    {
        if (type.IsEnum) return DbType.Int32;

        if (type.IsArray && type.GetElementType().IsEnum) return DbType.Object;

        if (SqlTypeMap.TryGetValue(type, out DbType dbType)) return dbType;

        throw new ArgumentException();
    }
}