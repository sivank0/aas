using AAS.Tools.DB.Enums;
using System.Reflection;

namespace AAS.Tools.DB.Mappers;

internal interface IPropertyMap
{
    String Name { get; }
    String ColumnName { get; }
    Boolean IsReadOnly { get; }
    Boolean IgnoreOnUpdate { get; }
    Boolean UpdateOnRemove { get; }
    KeyType KeyType { get; }
    PropertyInfo PropertyInfo { get; }
}
