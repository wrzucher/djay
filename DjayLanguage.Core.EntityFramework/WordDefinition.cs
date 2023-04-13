namespace DjayLanguage.Core.EntityFramework;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WordDefinition
{
    [Key]
    public int Id { get; set; }

    public int WordId { get; set; }

    [MaxLength(25)]
    public string WordType { get; set; } = null!;

    [MaxLength(2500)]
    public string Definition { get; set; } = null!;

    [MaxLength(800)]
    public string? Synonyms { get; set; }

    [MaxLength(800)]
    public string? Antonyms { get; set; }

    [MaxLength(40)]
    public string Source { get; set; } = null!;

    public Word Word { get; set; } = null!;

    [ForeignKey("WordDefinitionId")]
    public ICollection<WordExample> WordExamples { get; set; } = null!;
}

