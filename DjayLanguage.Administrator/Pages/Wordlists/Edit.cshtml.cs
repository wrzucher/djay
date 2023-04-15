namespace DjayLanguage.Administrator.Pages.Wordlists;

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
    /// Gets or sets candidates in worlists.
    /// </summary>
    [BindProperty]
    public string? Candidates { get; set; }

    /// <summary>
    /// Gets or sets candidates which not exist in word list.
    /// </summary>
    public IEnumerable<string>? NotInlistWords { get; set; }

    /// <summary>
    /// Gets or sets information about word group or null.
    /// </summary>
    [BindProperty]
    public WordGroup WordGroup { get; set; }

    public ActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return this.NotFound();
        }

        this.WordGroup = this.wordManager.GetWordGroup(id.Value);
        if (this.WordGroup is null)
        {
            return this.NotFound();
        }

        return this.Page();
    }

    public ActionResult OnPostAddWords()
    {
        if (this.WordGroup is null || this.Candidates == null)
        {
            return this.NotFound();
        }

        var candidates = this.Candidates
            .Split(new[] { ",", ";", Environment.NewLine },StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => _.Trim().ToUpperInvariant())
            .ToList();
        this.NotInlistWords = this.wordManager.AddWordlistToGroup(this.WordGroup.Id, candidates);
        this.WordGroup = this.wordManager.GetWordGroup(this.WordGroup.Id);

        if (this.NotInlistWords.Count() != this.Candidates.Count())
        {
            this.ErrorCode = ServiceErrorCode.Ok;
        }   
        
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

        var result = this.wordManager.UpdateWordGroup(this.WordGroup.Id, this.WordGroup.Name);
        this.WordGroup = this.wordManager.GetWordGroup(this.WordGroup.Id);
        this.ErrorCode = result;
        return this.Page();
    }

}
