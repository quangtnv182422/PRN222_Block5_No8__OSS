using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class GeminiController : ControllerBase
{
    private readonly GeminiService _gemini;

    public GeminiController(GeminiService gemini)
    {
        _gemini = gemini;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] string prompt )
    {
        if (string.IsNullOrWhiteSpace(prompt)) return BadRequest("Prompt cannot be empty");

        var result = await _gemini.AskGeminiAsync(prompt, HttpContext);
        return Ok(result);
    }

}
