using BestNewsApi.Interfaces;
using BestNewsApi.Models;

namespace BestNewsApi.Services;

public class ArticleService : IArticleService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ArticleService(IHttpClientFactory httpClientFactory) 
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Story> GetStory(long id)
    {

        var httpClient = _httpClientFactory.CreateClient("BestNewApi");
        var hackerNewsStory = await httpClient.GetFromJsonAsync<HackerNewsStory>($"https://hacker-news.firebaseio.com/v0/item/{id}.json");

        return hackerNewsStory == null ? new Story() { Title = "Failed to get story" } :
            new Story() 
                { 
                    Title = hackerNewsStory?.Title ?? string.Empty,
                    PostedBy = hackerNewsStory?.By ?? string.Empty,
                    Uri = hackerNewsStory?.Url ?? string.Empty,
                    Time = DateTimeOffset.FromUnixTimeSeconds(hackerNewsStory!.Time).ToString("u"),
                    CommentCount = hackerNewsStory.Descendants,
                    Score = hackerNewsStory.Score,
                };
    }
}
