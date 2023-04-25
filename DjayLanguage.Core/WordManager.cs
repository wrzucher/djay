namespace DjayLanguage.Core;

using AutoMapper;
using DjayLanguage.Core.EntityFramework;
using DjayLanguage.Core.ObjectModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
    /// Gets shourt information about all word groups.
    /// </summary>
    /// <returns>Ordered sequence of <see cref="ObjectModels.WordGroup"/></returns>
    public IList<ObjectModels.WordGroup> GetWordGroups()
    {
        var wordGroups = this.djayDbContext.WordGroups.OrderBy(_ => _.Name);
        var models = this.mapper.Map<IList<ObjectModels.WordGroup>>(wordGroups);
        return models;
    }

    /// <summary>
    /// Gets word group by id with wordlists.
    /// </summary>
    /// <param name="wordGroupId">Id of word group.</param>
    /// <returns>Information about word group with wordlist in class <see cref="ObjectModels.WordGroup"/></returns>
    public ObjectModels.WordGroup? GetWordGroup(int wordGroupId)
    {
        var wordGroups = this.djayDbContext.WordGroups
            .Include(_ => _.Wordlists).ThenInclude(_ => _.Word)
            .FirstOrDefault(_ => _.Id == wordGroupId);
        var models = this.mapper.Map<ObjectModels.WordGroup?>(wordGroups);
        return models;
    }

    /// <summary>
    /// Create words from list.
    /// </summary>
    /// <param name="wordlist">Wordlist which should be created.</param>
    /// <returns>Collection of word which not found and not added to group.</returns>
    public ServiceErrorCode CreateWords(IList<string> wordlist)
    {
        var existingWords = this.djayDbContext.Words.Where(_ => wordlist.Contains(_.Name)).ToList();
        var existingWordIds = new HashSet<string>(existingWords.Select(_ => _.Name));
        var wordlistToAdd = wordlist.Where(_ => !existingWordIds.Contains(_)).ToList();

        var wordlistRecords = wordlistToAdd.Select(_ => new EntityFramework.Word()
        {
            Name = _,
        });

        if (wordlistRecords.Any())
        {
            this.djayDbContext.Words.AddRange(wordlistRecords);
            this.djayDbContext.SaveChanges();
        }

        return ServiceErrorCode.Ok;
    }

    /// <summary>
    /// Add wordlist to word group.
    /// </summary>
    /// <param name="wordGroupId">Id of word group.</param>
    /// <param name="wordlist">Wordlist which should be added to group.</param>
    /// <returns>Collection of word which not found and not added to group.</returns>
    public IEnumerable<string> AddWordlistToGroup(int wordGroupId, IList<string> wordlist)
    {
        var words = this.djayDbContext.Words.Where(_ => wordlist.Contains(_.Name)).ToList();

        var wordlistRecords = words.Select(_ => new EntityFramework.Wordlist() {
            WordId = _.Id,
            WordGroupId = wordGroupId,
        }).ToList();

        var existingWords = this.djayDbContext.Wordlists.Where(_ => _.WordGroupId == wordGroupId).Select(_ => _.WordId);
        var existingWordIds = new HashSet<int>(existingWords);
        var wordlistToAdd = wordlistRecords.Where(_ => !existingWordIds.Contains(_.WordId)).ToList();

        if (wordlistToAdd.Any())
        {
            this.djayDbContext.Wordlists.AddRange(wordlistToAdd);
            this.djayDbContext.SaveChanges();
        }

        return wordlist.Where(_ => !words.Any(w => w.Name == _));
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
    /// Delete word by id with all dependencies.
    /// </summary>
    /// <param name="id">Id of the word which should be deleted.</param>
    /// <returns>Result of operation <see cref="ObjectModels.ServiceErrorCode"/></returns>
    public ServiceErrorCode DeleteWord(int id)
    {
        var word = this.djayDbContext.Words
            .First(_ => _.Id == id);
        this.djayDbContext.Words.Remove(word);
        this.djayDbContext.SaveChanges();
        return ServiceErrorCode.Ok;
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
    /// Create new word group.
    /// </summary>
    /// <param name="groupName">New name of the word group.</param>
    /// <returns>Result of operation <see cref="ObjectModels.ServiceErrorCode"/></returns>
    public ServiceErrorCode CreateWordGroup(string groupName)
    {
        var wordGroupByName = this.GetWordGroupByName(groupName);
        if (wordGroupByName is not null)
        {
            return ServiceErrorCode.WordAlreadyExist;
        }

        var newGroup = new EntityFramework.WordGroup();
        newGroup.Name = groupName;
        this.djayDbContext.WordGroups.Add(newGroup);
        this.djayDbContext.SaveChanges();
        return ServiceErrorCode.Ok;
    }

    /// <summary>
    /// Update existing word group.
    /// </summary>
    /// <param name="id">Id of the word group which should be updated.</param>
    /// <param name="groupName">New name of the word group.</param>
    /// <returns>Result of operation <see cref="ObjectModels.ServiceErrorCode"/></returns>
    public ServiceErrorCode UpdateWordGroup(int id, string newGroupName)
    {
        var wordGroupByName = this.GetWordGroupByName(newGroupName);
        if (wordGroupByName is not null && wordGroupByName.Id != id)
        {
            return ServiceErrorCode.WordAlreadyExist;
        }

        var group = this.djayDbContext.WordGroups.First(_ => _.Id == id);
        group.Name = newGroupName;
        this.djayDbContext.SaveChanges();
        return ServiceErrorCode.Ok;
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
    /// Get word by name.
    /// </summary>
    /// <param name="wordName">Name of the word.</param>
    /// <returns>Information about word in the class <see cref="ObjectModels.Word"/></returns>
    public ObjectModels.Word? GetWordByName( string wordName)
    {
        var word = this.djayDbContext.Words.FirstOrDefault(_ => _.Name == wordName);
        var model = this.mapper.Map<ObjectModels.Word?>(word);
        return model;
    }

    /// <summary>
    /// Get word group by name.
    /// </summary>
    /// <param name="wordGroupName">Name of the word group.</param>
    /// <returns>Information about word group in the class <see cref="ObjectModels.WordGroup"/></returns>
    public ObjectModels.WordGroup? GetWordGroupByName(string wordGroupName)
    {
        var word = this.djayDbContext.WordGroups.FirstOrDefault(_ => _.Name == wordGroupName);
        var model = this.mapper.Map<ObjectModels.WordGroup?>(word);
        return model;
    }
}
