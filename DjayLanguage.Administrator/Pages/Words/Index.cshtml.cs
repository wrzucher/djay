namespace DjayLanguage.Administrator.Pages.Words;

using DjayLanguage.Core;
using DjayLanguage.Core.ObjectModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class IndexModel : PageModel
{
    private const int DefaultPageSize = 20;

    private readonly ILogger<IndexModel> logger;
    private readonly WordManager wordManager;

    public IndexModel(WordManager wordManager, ILogger<IndexModel> logger)
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

    /// <summary>
    /// Gets or sets candidates in worlists.
    /// </summary>
    [BindProperty]
    public string? Candidates { get; set; }

    /// <summary>
    /// Gets or sets information about word or null.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public ServiceErrorCode? ErrorCode { get; private set; }

    public IList<Word> Words { get; private set; }

    public void OnGet()
    {
        this.Words = this.wordManager.GetWords(this.SearchText, 0, this.PageNumber, this.PageSize);
    }
    public ActionResult OnPostAddWords()
    {
        if (this.Candidates == null)
        {
            return this.NotFound();
        }

        var candidates = this.Candidates
            .Split(new[] { ",", ";", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => _.Trim().ToUpperInvariant())
            .ToList();
        this.ErrorCode = this.wordManager.CreateWords(candidates);
        this.Words = this.wordManager.GetWords(this.SearchText, 0, this.PageNumber, this.PageSize);
        return this.Page();
    }
}
