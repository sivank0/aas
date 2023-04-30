#region

using System.Collections.Concurrent;
using System.Data;
using AAS.Tools.DB.Enums;
using AAS.Tools.DB.Mappers;

#endregion

namespace AAS.Tools.DB.Common;

internal static class IDbCommandExtensions
{
    private static readonly ConcurrentDictionary<Type, ISqlParameterMap> _parameterMaps = new();

    private static ISqlParameterMap GetAndSetSqlParameterMap(object value)
    {
        Type type = value.GetType();

        if (_parameterMaps.TryGetValue(type, out ISqlParameterMap parameterMap)) return parameterMap;

        parameterMap = new SqlParameterMap(type, TypeMap.LookupDbType(type));

        _parameterMaps[type] = parameterMap;

        return parameterMap;
    }

    private static IDbDataParameter GetDbNullParameter(this IDbCommand command, string nameParameter)
    {
        IDbDataParameter parameter = command.CreateParameter();
        parameter.ParameterName = nameParameter;
        parameter.Value = DBNull.Value;

        return parameter;
    }

    private static IDbDataParameter GetParameter(this IDbCommand command, string parameterName, object value,
        ISqlParameterMap parameterMap)
    {
        IDbDataParameter parameter = command.CreateParameter();
        parameter.ParameterName = parameterName;

        parameter.Value = MapperParameters.GetParameterValue(parameterMap.ParameterType, value);

        parameter.DbType = parameterMap.DbType;

        return parameter;
    }


    public static void AddParameters(this IDbCommand command, IList<SqlParameter> parameters)
    {
        foreach (SqlParameter sqlParameter in parameters)
        {
            if (sqlParameter.Value == null)
            {
                IDbDataParameter nullParameter = command.GetDbNullParameter(sqlParameter.Name);
                command.Parameters.Add(nullParameter);
                continue;
            }

            ISqlParameterMap parameterMap = GetAndSetSqlParameterMap(sqlParameter.Value);

            IDbDataParameter parameter = command.GetParameter(sqlParameter.Name, sqlParameter.Value,
                parameterMap);

            command.Parameters.Add(parameter);
        }
    }

    private static void AddParameter<T>(this IDbCommand command, IPropertyMap propertyMap, T value)
    {
        object obj = propertyMap.PropertyInfo.GetValue(value);

        string parameterName = $"@{propertyMap.ColumnName}";

        if (obj == null)
        {
            IDbDataParameter nullParameter = command.GetDbNullParameter(parameterName);
            command.Parameters.Add(nullParameter);
            return;
        }

        ISqlParameterMap parameterMap = GetAndSetSqlParameterMap(obj);

        IDbDataParameter parameter = command.GetParameter(parameterName, obj,
            parameterMap);

        command.Parameters.Add(parameter);
    }

    public static void AddParametersForInsert<T>(this IDbCommand command, T value, ClassMapper classMapper)
    {
        foreach (IPropertyMap propertyMap in classMapper.Properties)
        {
            if (propertyMap.IsReadOnly) continue;

            command.AddParameter(propertyMap, value);
        }
    }

    public static void AddParametersForUpdate<T>(this IDbCommand command, T value, ClassMapper classMapper)
    {
        foreach (IPropertyMap propertyMap in classMapper.Properties)
        {
            if (propertyMap.IsReadOnly || propertyMap.IgnoreOnUpdate) continue;

            command.AddParameter(propertyMap, value);
        }
    }

    public static void AddParametersForRemoveByUpdate<T>(this IDbCommand command, T value, ClassMapper classMapper)
    {
        foreach (IPropertyMap propertyMap in classMapper.Properties)
        {
            if (propertyMap.IsReadOnly) continue;

            if (!propertyMap.UpdateOnRemove && propertyMap.KeyType != KeyType.Identity) continue;

            command.AddParameter(propertyMap, value);
        }
    }

    public static void AddParametersForRemove<T>(this IDbCommand command, T value, ClassMapper classMapper)
    {
        foreach (IPropertyMap propertyMap in classMapper.Properties)
        {
            if (propertyMap.IsReadOnly) continue;

            if (propertyMap.KeyType != KeyType.Identity) continue;

            command.AddParameter(propertyMap, value);
        }
    }
}