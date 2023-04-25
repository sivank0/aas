#region

using AAS.Tools.DB.Enums;

#endregion

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