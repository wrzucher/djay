namespace DjayLanguage.Core;

using AutoMapper;
using DjayLanguage.Core.EntityFramework;
using DjayLanguage.Core.ObjectModels;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The manager for the word context.
/// </summary>
public class WordManager
{
    private readonly DjayDbContext djayDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="WordManager"/> class.
    /// </summary>
    /// <param name="djayDbContext">Main database context.</param>
    public WordManager(DjayDbContext djayDbContext, IMapper mapper)
    {
        this.djayDbContext = djayDbContext ?? throw new ArgumentNullException(nameof(djayDbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Gets words by parameters.
    /// </summary>
    /// <param name="searchText">Text which should be found in the returned collection.</param>
    /// <param name="offset">Offset for returned collection.</param>
    /// <param name="page">Number of page for returned collection.</param>
    /// <param name="pageSize">Page size for returned collection.</param>
    /// <returns>Ordered sequence of <see cref="ObjectModels.Word"/></returns>
    public IList<ObjectModels.Word> GetWords(string? searchText, int offset, int page, int pageSize)
    {
        var words = this.djayDbContext.Words.OrderBy(_ => _.Name).AsQueryable();
        if (searchText is not null)
        {
            words = words.Where(_ => _.Name.Contains(searchText));
        }

        words = words.Page(offset, page, pageSize);
        var models = this.mapper.Map<IList<ObjectModels.Word>>(words);
        return models;
    }

    /// <summary>
    /// Gets all definitions for word.
    /// </summary>
    /// <param name="wordId">Id of word for which return all definitions.</param>
    /// <returns>Ordered sequence of <see cref="ObjectModels.WordDefinition"/></returns>
    public IList<ObjectModels.WordDefinition> GetWordDefinitions(int wordId)
    {
        var wordDefinitions = this.djayDbContext.WordDefinitions
            .Include(_ => _.WordExamples)
            .Where(_ => _.WordId == wordId)
            .OrderBy(_ => _.Id);
        var models = this.mapper.Map<IList<ObjectModels.WordDefinition>>(wordDefinitions);
        return models;
    }

    /// <summary>
    /// Gets all examples for word.
    /// </summary>
    /// <param name="wordDefinitionId">Id of word definition for which return all examples.</param>
    /// <returns>Ordered sequence of <see cref="ObjectModels.WordExample"/></returns>
    public IList<ObjectModels.WordExample> GetWordExamples(int wordDefinitionId)
    {
        var wordDefinitions = this.djayDbContext.WordExamples
            .Where(_ => _.WordDefinitionId == wordDefinitionId)
            .OrderBy(_ => _.Id);
        var models = this.mapper.Map<IList<ObjectModels.WordExample>>(wordDefinitions);
        return models;
    }

    /// <summary>
    /// Gets word by Id.
    /// </summary>
    /// <param name="id">Id of word.</param>
    /// <returns>Information about word in the class <see cref="ObjectModels.Word"/></returns>
    public ObjectModels.Word? GetWord(int id)
    {
        var word = this.djayDbContext.Words.FirstOrDefault(_ => _.Id == id);
        var model = this.mapper.Map<ObjectModels.Word?>(word);
        return model;
    }

    /// <summary>
    /// Update existing word.
    /// </summary>
    /// <param name="id">Id of the word.</param>
    /// <param name="wordName">New name of the word.</param>
    /// <returns>Result of operation <see cref="ObjectModels.ServiceErrorCode"/></returns>
    public ServiceErrorCode UpdateWord(int id, string wordName)
    {
        var wordByName = this.GetWordByName(wordName);
        if (wordByName is not null && wordByName.Id != id)
        {
            return ServiceErrorCode.WordAlreadyExist;
        }

        var word = this.djayDbContext.Words.First(_ => _.Id == id);
        word.Name = wordName;
        this.djayDbContext.SaveChanges();
        return ServiceErrorCode.Ok;
    }

    /// <summary>
    /// Get word by his name.
    /// </summary>
    /// <param name="wordName">New name of the word.</param>
    /// <returns>Information about word in the class <see cref="ObjectModels.Word"/></returns>
    public ObjectModels.Word? GetWordByName( string wordName)
    {
        var word = this.djayDbContext.Words.FirstOrDefault(_ => _.Name == wordName);
        var model = this.mapper.Map<ObjectModels.Word?>(word);
        return model;
    }
}
