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
        var story = await httpClient.GetFromJsonAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{id}.json");

        return story ?? new Story() {  Id = id, Title = "Failed to get"};
    }
}
