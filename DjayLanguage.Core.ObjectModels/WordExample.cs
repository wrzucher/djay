namespace DjayLanguage.Core.ObjectModels;

/// <summary>
/// The class which contains information about an example of a word using.
/// Example related to word definition.
/// </summary>
public class WordExample
{
    /// <summary>
    /// Gets or sets id of word example.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets id of word definition which related to word. 
    /// </summary>
    public int WordDefinitionId { get; set; }

    /// <summary>
    /// Gets or sets an example of a word using.
    /// </summary>
    public string Example { get; set; } = null!;
}

