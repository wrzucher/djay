using DjayLanguage.Integration.OpenAI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Djay.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> logger;
        private readonly OpenAIManager openAIManager;

        public WeatherForecastController(OpenAIManager openAIManager, ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
            this.openAIManager = openAIManager ?? throw new ArgumentNullException(nameof(OpenAIManager));
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await this.openAIManager.GetAnswerAsync();
        }
    }
}