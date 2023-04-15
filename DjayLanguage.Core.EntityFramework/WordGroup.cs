namespace DjayLanguage.Core.EntityFramework;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WordGroup
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [ForeignKey("WordGroupId")]
    public ICollection<Wordlist> Wordlists { get; set; } = null!;
}

