using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using BestNewsApi.Interfaces;
using BestNewsApi.Models;
using BestNewsApi.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BestNewsTests;

public class StoryTests
{
    [Fact]
    public async void GetBestStories_ReturnsScoreDesc()
    {
        var handler = A.Fake<HttpMessageHandler>();

        var storyIds = new long[] { 1, 3, 13, 109 };
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(storyIds)
        };

        var articleService = A.Fake<IArticleService>();

        A.CallTo(handler)
            .WithReturnType<Task<HttpResponseMessage>>()
            .Returns(Task.FromResult(response));

        A.CallTo(() => articleService.GetStory(storyIds[0])).Returns(new Story { Score = 3 });
        A.CallTo(() => articleService.GetStory(storyIds[1])).Returns(new Story { Score = 1 });
        A.CallTo(() => articleService.GetStory(storyIds[2])).Returns(new Story { Score = 99 });
        A.CallTo(() => articleService.GetStory(storyIds[3])).Returns(new Story { Score = 7 });

        var httpClient = new HttpClient(handler);
        var httpClientFactory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => httpClientFactory.CreateClient("BestNewApi")).Returns(httpClient);

        IConfiguration config = A.Fake<IConfiguration>();
        ILogger<StoryService> logger = A.Fake<ILogger<StoryService>>();

        int noOfStories = 3;
        var _sut = new StoryService(httpClientFactory, articleService, logger);

        var results = await _sut.GetBestStories(noOfStories);

        results?.Count.Should().Be(noOfStories);
        results.Should().BeInDescendingOrder(x => x.Score);
    }
}