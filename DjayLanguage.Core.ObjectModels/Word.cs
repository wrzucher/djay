namespace DjayLanguage.Core.ObjectModels;

/// <summary>
/// The class which contains information about a word.
/// </summary>
public class Word
{
    /// <summary>
    /// Gets or sets id of a word.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets a word itself.
    /// </summary>
    public string Name { get; set; } = null!;
}

