namespace DjayLanguage.Administrator.Pages.Wordlists;

using DjayLanguage.Core;
using DjayLanguage.Core.ObjectModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreateModel : PageModel
{
    private readonly ILogger<EditModel> logger;
    private readonly WordManager wordManager;

    public CreateModel(WordManager wordManager, ILogger<EditModel> logger)
    {
        this.wordManager = wordManager;
        this.logger = logger;
    }

    /// <summary>
    /// Gets or sets information about word or null.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public ServiceErrorCode? ErrorCode { get; private set; }

    /// <summary>
    /// Gets or sets information about word group or null.
    /// </summary>
    [BindProperty]
    public WordGroup WordGroup { get; set; }

    public ActionResult OnGet()
    {
        return this.Page();
    }

    public ActionResult OnPost()
    {
        if (this.WordGroup is null)
        {
            return this.NotFound();
        }

        if (!ModelState.IsValid)
        {
            return this.Page();
        }

        var result = this.wordManager.CreateWordGroup(this.WordGroup.Name);
        return this.RedirectToPage("./Index", new { errorCode = result });
    }
}
