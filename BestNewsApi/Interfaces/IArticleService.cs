using BestNewsApi.Models;

namespace BestNewsApi.Interfaces;

public interface IArticleService
{
    public Task<Story> GetStory(long id);
}
