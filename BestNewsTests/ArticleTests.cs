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
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace BestNewsTests;

public class ArticleTests
{
    [Fact]
    public async Task GetStory_ReturnsCorrectIdAsync()
    {
        int testId = 7;
        Story story = new Story() { Id = testId };

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(story)
        };

        var handler = A.Fake<HttpMessageHandler>();

        A.CallTo(handler)
                .WithReturnType<Task<HttpResponseMessage>>()
                .Returns(Task.FromResult(response));

        var httpClient = new HttpClient(handler);
        var httpClientFactory = A.Fake<IHttpClientFactory>();
        A.CallTo(() => httpClientFactory.CreateClient("BestNewApi")).Returns(httpClient);

        var _sut = new ArticleService(httpClientFactory);

        var result = await _sut.GetStory(testId);

        result?.Id.Should().Be(testId);
    }
}
