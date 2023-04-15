namespace DjayLanguage.Administrator.Pages.Words;

using DjayLanguage.Core;
using DjayLanguage.Core.ObjectModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> logger;
    private readonly WordManager wordManager;

    public EditModel(WordManager wordManager, ILogger<EditModel> logger)
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
    /// Gets or sets information about word or null.
    /// </summary>
    [BindProperty]
    public Word? WordInformation { get; set; }

    /// <summary>
    /// Gets or sets information about word definitions or null.
    /// </summary>
    public IList<WordDefinition>? WordDefinitions { get; private set; }

    public ActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        this.WordInformation = this.wordManager.GetWord(id.Value);
        if (this.WordInformation is null)
        {
            return NotFound();
        }

        this.WordDefinitions = this.wordManager.GetWordDefinitions(id.Value);
        return Page();
    }

    public ActionResult OnPost()
    {
        if (this.WordInformation is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            this.WordDefinitions = this.wordManager.GetWordDefinitions(this.WordInformation.Id);
            return Page();
        }

        var wordExist = this.wordManager.GetWord(this.WordInformation.Id);
        if (wordExist is null)
        {
            return NotFound();
        }

        var result = this.wordManager.UpdateWord(this.WordInformation.Id, this.WordInformation.Name);
        this.ErrorCode = result;
        this.WordInformation = this.wordManager.GetWord(this.WordInformation.Id);
        this.WordDefinitions = this.wordManager.GetWordDefinitions(this.WordInformation.Id);
        return Page();
    }
}
