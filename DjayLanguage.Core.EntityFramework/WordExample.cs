namespace DjayLanguage.Core.EntityFramework;

using System.ComponentModel.DataAnnotations;

public class WordExample
{
    [Key]
    public int Id { get; set; }

    public int WordDefinitionId { get; set; }

    [MaxLength(500)]
    public string Example { get; set; } = null!;

    public WordDefinition WordDefinition { get; set; } = null!;
}

