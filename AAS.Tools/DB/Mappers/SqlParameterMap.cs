﻿using System.Data;

namespace AAS.Tools.DB.Mappers;

internal class SqlParameterMap : ISqlParameterMap
{
    public Type ParameterType { get; }
    public DbType DbType { get; }

    public SqlParameterMap(Type type, DbType dbType)
    {
        ParameterType = type;
        DbType = dbType;
    }
}
