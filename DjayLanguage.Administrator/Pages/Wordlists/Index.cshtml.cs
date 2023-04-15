namespace DjayLanguage.Administrator.Pages.Wordlists;

using DjayLanguage.Core;
using DjayLanguage.Core.ObjectModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly WordManager wordManager;

    public IndexModel(WordManager wordManager, ILogger<IndexModel> logger)
    {
        this.wordManager = wordManager;
        this.logger = logger;
    }

    /// <summary>
    /// Gets or sets information about operation or null.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public ServiceErrorCode? ErrorCode { get; private set; }

    public IList<WordGroup> WordGroups { get; private set; }

    public void OnGet()
    {
        this.WordGroups = this.wordManager.GetWordGroups();
    }
}
