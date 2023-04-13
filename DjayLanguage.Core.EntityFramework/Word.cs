namespace DjayLanguage.Core.EntityFramework;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Word
{
    [Key]
    public int Id { get; set; }

    [MaxLength(45)]
    public string Name { get; set; } = null!;

    [ForeignKey("WordId")]
    public ICollection<WordDefinition> WordDefinitions { get; set; } = null!;
}

