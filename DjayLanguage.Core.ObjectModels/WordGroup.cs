namespace DjayLanguage.Core.ObjectModels;

/// <summary>
/// The class which contains information about word groups.
/// </summary>
public class WordGroup
{
    /// <summary>
    /// Gets or sets id of group.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets name of the group.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets words in this group.
    /// </summary>
    public IList<Wordlist>? Wordlists { get; set; }
}

