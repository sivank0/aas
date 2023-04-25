#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using AAS.Tools.DB.Enums;

#endregion

namespace AAS.Tools.DB.Mappers;

class ClassMapper : IMapper
{
    public string TableName { get; }

    public MapperType Type => MapperType.Class;

    public Type EntityType { get; }

    public IPropertyMap[] Properties { get; }

    public ClassMapper(Type type)
    {
        EntityType = type;
        TableName = type.GetCustomAttribute<TableAttribute>()?.Name;
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (properties.Length == 0) throw new ArgumentException("Count properties = 0");

        Properties = properties.Select(property => new PropertyMap(property)).ToArray();
    }

    public Dictionary<int, PropertyInfo> Mappings(IDataRecord record)
    {
        IEnumerable<int> columns = Enumerable.Range(0, record.FieldCount);
        var properties = Properties
            .Select(x => new
            {
                name = x.ColumnName,
                prop = x.PropertyInfo
            });
        return columns
            .Join(properties, record.GetName, x => x.name, (index, x) => new
            {
                index,
                prop = !x.prop.CanWrite ? null : x.prop
            }, StringComparer.InvariantCultureIgnoreCase)
            .Where(x => x.prop != null) // only settable properties accounted for
            .ToDictionary(x => x.index, x => x.prop);
    }
}