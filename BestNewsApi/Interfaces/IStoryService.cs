using BestNewsApi.Models;

namespace BestNewsApi.Interfaces;

public interface IStoryService
{
    public Task<List<Story>> GetBestStories(int noOfStories);
}
