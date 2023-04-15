namespace DjayLanguage.Core.ObjectModels;

/// <summary>
/// The class which contains information about wordlists.
/// </summary>
public class Wordlist
{
    /// <summary>
    /// Gets or sets id of wordlists.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets id of word.
    /// </summary>
    public int WordId { get; set; }

    /// <summary>
    /// Gets or sets id of word group.
    /// </summary>
    public int WordGroupId { get; set; }

    /// <summary>
    /// Gets or sets word object.
    /// </summary>
    public Word Word { get; set; } = null!;

    /// <summary>
    /// Gets or sets word group object.
    /// </summary>
    public WordGroup WordGroup { get; set; } = null!;
}

