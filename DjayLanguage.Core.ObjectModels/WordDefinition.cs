namespace DjayLanguage.Core.ObjectModels;

/// <summary>
/// The class which contains information about word definition.
/// </summary>
public class WordDefinition
{
    /// <summary>
    /// Gets or sets id of word definition. 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets id of a word.
    /// </summary>
    public int WordId { get; set; }

    /// <summary>
    /// Gets or sets type of a word, like verb, noun etc.
    /// </summary>
    public string WordType { get; set; } = null!;

    /// <summary>
    /// Gets or sets definition of a word.
    /// </summary>
    public string Definition { get; set; } = null!;

    /// <summary>
    /// Gets or sets synonyms of a word.
    /// </summary>
    public string? Synonyms { get; set; }

    /// <summary>
    /// Gets or sets antonyms of a word.
    /// </summary>
    public string? Antonyms { get; set; }

    /// <summary>
    /// Gets or sets source of definition.
    /// </summary>
    public string Source { get; set; } = null!;
}

