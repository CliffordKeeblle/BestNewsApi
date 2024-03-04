using System.Collections.Concurrent;
using BestNewsApi.Interfaces;
using BestNewsApi.Models;

namespace BestNewsApi.Services;

public class StoryService : IStoryService
{
    private ConcurrentDictionary<long, Story> _storiesCache = new ConcurrentDictionary<long, Story>();

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IArticleService _articleService;
    private readonly ILogger<StoryService> _logger;

    public StoryService(
    IHttpClientFactory httpClientFactory,
    IArticleService articleService,
    ILogger<StoryService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _articleService = articleService;
        _logger = logger;
    }

    public async Task<List<Story>> GetBestStories(int noOfStories)
    {
        var httpClient = _httpClientFactory.CreateClient("BestNewApi");
        var bestStories = await httpClient.GetFromJsonAsync<List<long>>("https://hacker-news.firebaseio.com/v0/beststories.json");

        List<Story> stories = await GetStories(bestStories);

        return stories.OrderByDescending(s => s.Score).Take(noOfStories).ToList(); ;
    }

    async Task<List<Story>> GetStories(List<long>? storyIds)
    {
        List<Story> stories = new List<Story>();
        if (storyIds?.Any() ?? false)
        {
            foreach (var storyId in storyIds)
            {
                var story = await GetStory(storyId);
                stories.Add(story);
            }
        }

        _logger.LogInformation($"Fetched {stories.Count} Stories in total");

        return stories;
    }

    public async Task<Story> GetStory(long id)
    {
        if (_storiesCache.TryGetValue(id, out Story? story))
        {
            return story;
        }

        story = await _articleService.GetStory(id);
        _storiesCache.AddOrUpdate(id, story, (id, s) => story);

        return story;
    }
}
