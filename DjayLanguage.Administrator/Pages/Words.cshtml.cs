namespace DjayLanguage.Administrator.Pages;

using DjayLanguage.Core;
using DjayLanguage.Core.ObjectModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class WordsModel : PageModel
{
    private const int DefaultPageSize = 20;

    private readonly ILogger<IndexModel> logger;
    private readonly WordManager wordManager;

    public WordsModel(WordManager wordManager, ILogger<IndexModel> logger)
    {
        this.wordManager = wordManager;
        this.logger = logger;
    }

    /// <summary>
    /// Gets or sets number of page for returned collection.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets page size for returned collection.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    [Range(5, 100)]
    public int PageSize { get; set; } = DefaultPageSize;

    /// <summary>
    /// Gets or sets search string.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string SearchText { get; set; }

    public IList<Word> Words { get; private set; }

    public void OnGet()
    {
        this.Words = this.wordManager.GetWords(this.SearchText, 0, this.PageNumber, this.PageSize);
    }
}
