using OpenAI_API;

namespace DjayLanguage.Integration.OpenAI;

public class OpenAIManager
{
    private readonly OpenAIAPI openAIAPI;

    public OpenAIManager(OpenAIAPI openAIAPI)
    {
        this.openAIAPI = openAIAPI ?? throw new ArgumentNullException(nameof(OpenAIAPI));
    }

    public async Task<string> GetAnswerAsync()
    {
        var result = await this.openAIAPI.Completions.GetCompletion("One Two Three One Two");
        return result;
    }
}
