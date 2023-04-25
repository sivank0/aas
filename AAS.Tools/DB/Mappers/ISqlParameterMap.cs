#region

using System.Data;

#endregion

namespace AAS.Tools.DB.Mappers;

internal interface ISqlParameterMap
{
    Type ParameterType { get; }
    DbType DbType { get; }
}