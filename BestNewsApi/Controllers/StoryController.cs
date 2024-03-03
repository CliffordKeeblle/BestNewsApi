using BestNewsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BestNewsApi.Controllers;

[ApiController]
[Route("api/stories")]
public class StoryController : ControllerBase
{
    private readonly ILogger<StoryController> _logger;
    private readonly IStoryService _storyService;

    public StoryController(IStoryService StoryService, ILogger<StoryController> logger)
    {
        _storyService = StoryService;
        _logger = logger;
    }

    [HttpGet("best")]
    public async Task<IActionResult> GetListOfBestStories(int n)
    {
        if (n <= 0)
        {
            return BadRequest("Given value is invalid");
        }

        var stories = await _storyService.GetBestStories(n);
        return Ok(stories);
    }
}
