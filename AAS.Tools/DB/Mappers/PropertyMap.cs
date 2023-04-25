#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using AAS.Tools.DB.Attributes;
using AAS.Tools.DB.Enums;

#endregion

namespace AAS.Tools.DB.Mappers;

internal class PropertyMap : IPropertyMap
{
    public string Name => PropertyInfo.Name;

    public string ColumnName { get; }

    public bool IsReadOnly { get; }

    public bool IgnoreOnUpdate { get; }

    public bool UpdateOnRemove { get; }

    public KeyType KeyType { get; }

    public PropertyInfo PropertyInfo { get; }


    public PropertyMap(PropertyInfo propertyInfo)
    {
        PropertyInfo = propertyInfo;

        ColumnName = PropertyInfo.GetCustomAttribute<ColumnAttribute>()?.Name?.ToLower() ?? propertyInfo.Name.ToLower();
        IsReadOnly = PropertyInfo.GetCustomAttribute<IsReadOnlyAttribute>() != null;
        IgnoreOnUpdate = propertyInfo.GetCustomAttribute<IgnoreOnUpdateAttribute>() != null;
        UpdateOnRemove = propertyInfo.GetCustomAttribute<UpdateOnRemoveAttribute>() != null;
        IdentifierAttribute identifierAttribute = PropertyInfo.GetCustomAttribute<IdentifierAttribute>();

        if (IsReadOnly && identifierAttribute != null) throw new ArgumentException($"Property {Name} is readOnly");

        KeyType = identifierAttribute != null ? KeyType.Identity : KeyType.NotAKey;
    }
}