using AAS.Tools.DB.Enums;
using AAS.Tools.DB.Mappers;

namespace AAS.Tools.DB.Mappers;

internal class SimpleTypeMapper : IMapper
{
    public MapperType Type => MapperType.SimpleType;

    public Type EntityType { get; }

    public SimpleTypeMapper(Type entityType)
    {
        EntityType = entityType;
    }
}
