#region

using System.Text;
using AAS.Tools.DB.Enums;
using AAS.Tools.DB.Mappers;

#endregion

namespace AAS.Tools.DB;

internal static class SqlBuilder
{
    public static string CreateInsertCommand(this ClassMapper classMapper)
    {
        if (string.IsNullOrWhiteSpace(classMapper.TableName))
            throw new ArgumentNullException("Table name is null");

        return $"insert into {classMapper.TableName} {GetPropertiesListForInsert(classMapper.Properties)}";
    }

    private static string GetPropertiesListForInsert(IPropertyMap[] propertyMaps)
    {
        StringBuilder fields = new StringBuilder("(");

        IEnumerable<string> listFields = propertyMaps
            .Where(property => !property.IsReadOnly)
            .Select(property => property.ColumnName);

        fields.Append(string.Join(",", listFields));
        fields.Append(") VALUES (");
        fields.Append(string.Join(",", listFields.Select(x => $"@{x}")));
        fields.Append(");");

        return fields.ToString();
    }

    public static string CreateUpdateCommand(this ClassMapper classMapper)
    {
        if (string.IsNullOrWhiteSpace(classMapper.TableName))
            throw new ArgumentNullException("Table name is null");

        return $"UPDATE {classMapper.TableName} set {GetPropertiesListForUpdate(classMapper.Properties)}";
    }

    public static string CreateRemoveByUpdateCommand(this ClassMapper classMapper)
    {
        if (string.IsNullOrWhiteSpace(classMapper.TableName))
            throw new ArgumentNullException("Table name is null");

        return
            $"UPDATE {classMapper.TableName} SET {GetPropertiesListForRemoveByUpdateCommand(classMapper.Properties)}";
    }

    public static string CreateRemoveCommand(this ClassMapper classMapper)
    {
        if (string.IsNullOrWhiteSpace(classMapper.TableName))
            throw new ArgumentNullException("Table name is null");

        return $"DELETE FROM {classMapper.TableName} WHERE {GetPropertiesListForRemoveCommand(classMapper.Properties)}";
    }

    private static string GetPropertiesListForRemoveCommand(IPropertyMap[] propertyMaps)
    {
        string[] identifiers = GetIdentifierColumnNames(propertyMaps);

        if (identifiers.Length < 1) throw new Exception("Unknown Identifier for object");

        StringBuilder stringBuilder = new StringBuilder($"{identifiers[0]} = @{identifiers[0]}");

        if (identifiers.Length > 1)
            for (int i = 1; i < identifiers.Length; i++)
                stringBuilder.Append($" AND {identifiers[i]} = @{identifiers[i]}");

        stringBuilder.Append(";");

        return stringBuilder.ToString();
    }


    private static string GetStringUpdateProperty(string property)
    {
        return $", {property} = @{property}";
    }

    private static string[] GetIdentifierColumnNames(IPropertyMap[] propertyMaps)
    {
        return propertyMaps.Where(property => property.KeyType == KeyType.Identity)
            .Select(property => property.ColumnName).ToArray();
    }

    private static string GetPropertiesListForRemoveByUpdateCommand(IPropertyMap[] propertyMaps)
    {
        string[] identifiers = GetIdentifierColumnNames(propertyMaps);

        if (identifiers.Length < 1) throw new Exception("Unknown Identifier for object");

        string[] properties = propertyMaps.Where(x => x.KeyType == KeyType.NotAKey && x.UpdateOnRemove && !x.IsReadOnly)
            .Select(property => property.ColumnName).ToArray();

        StringBuilder stringBuilder = new StringBuilder(" isremoved = true ");

        if (properties.Length != 0)
        {
            stringBuilder.Append(GetStringUpdateProperty(properties[0]));

            if (properties.Length > 1)
                for (int i = 1; i < properties.Length; i++)
                    stringBuilder.Append(GetStringUpdateProperty(properties[i]));
        }

        stringBuilder.Append(" WHERE ");

        stringBuilder.Append($"{identifiers[0]} = @{identifiers[0]}");

        if (identifiers.Length > 1)
            for (int i = 1; i < identifiers.Length; i++)
                stringBuilder.Append($" AND {identifiers[i]} = @{identifiers[i]}");

        stringBuilder.Append(";");

        return stringBuilder.ToString();
    }

    private static string GetPropertiesListForUpdate(IPropertyMap[] propertyMaps)
    {
        string[] identifiers = GetIdentifierColumnNames(propertyMaps);

        if (identifiers.Length < 1) throw new Exception("Unknown Identifier for object");

        string[] properties = propertyMaps
            .Where(x => x.KeyType == KeyType.NotAKey && !x.IgnoreOnUpdate && !x.IsReadOnly)
            .Select(property => property.ColumnName).ToArray();

        if (properties.Length < 1) throw new Exception("properties for update < 1");


        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append($"{properties[0]} = @{properties[0]}");

        if (properties.Length > 1)
            for (int i = 1; i < properties.Length; i++)
                stringBuilder.Append(GetStringUpdateProperty(properties[i]));

        stringBuilder.Append(" WHERE ");

        stringBuilder.Append($"{identifiers[0]} = @{identifiers[0]}");

        if (identifiers.Length > 1)
            for (int i = 1; i < identifiers.Length; i++)
                stringBuilder.Append($" AND {identifiers[i]} = @{identifiers[i]}");

        stringBuilder.Append(";");

        return stringBuilder.ToString();
    }
}