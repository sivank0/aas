#region

using System.Reflection;
using AAS.Tools.DB.Enums;

#endregion

namespace AAS.Tools.DB.Mappers;

internal interface IPropertyMap
{
    string Name { get; }
    string ColumnName { get; }
    bool IsReadOnly { get; }
    bool IgnoreOnUpdate { get; }
    bool UpdateOnRemove { get; }
    KeyType KeyType { get; }
    PropertyInfo PropertyInfo { get; }
}