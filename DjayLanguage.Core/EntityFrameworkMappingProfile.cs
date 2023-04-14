namespace DjayLanguage.Core;

using AutoMapper;

/// <summary>
/// The profile for mapping from entity framework models to object models.
/// </summary>
public class EntityFrameworkMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityFrameworkMappingProfile"/> class.
    /// </summary>
    public EntityFrameworkMappingProfile()
    {
        this.CreateMap<EntityFramework.Word, ObjectModels.Word>();
        this.CreateMap<EntityFramework.WordDefinition, ObjectModels.WordDefinition>();
        this.CreateMap<EntityFramework.WordExample, ObjectModels.WordExample>();
    }
}