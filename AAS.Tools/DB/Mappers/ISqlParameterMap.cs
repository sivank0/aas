using System.Data;

namespace AAS.Tools.DB.Mappers;

internal interface ISqlParameterMap
{
    Type ParameterType { get; }
    DbType DbType { get; }
}
