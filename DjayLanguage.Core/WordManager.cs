namespace DjayLanguage.Core;

using AutoMapper;
using DjayLanguage.Core.EntityFramework;

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
}
