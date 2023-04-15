namespace DjayLanguage.Core.EntityFramework;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Wordlist
{
    [Key]
    public int Id { get; set; }

    public int WordId { get; set; }

    public int WordGroupId { get; set; }

    public Word Word { get; set; } = null!;

    public WordGroup WordGroup { get; set; } = null!;
}

