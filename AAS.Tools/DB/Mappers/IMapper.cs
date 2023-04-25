#region

using AAS.Tools.DB.Enums;

#endregion

namespace AAS.Tools.DB.Mappers;

internal interface IMapper
{
    MapperType Type { get; }
    Type EntityType { get; }
}