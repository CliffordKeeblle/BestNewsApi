namespace BestNewsApi.Models;

public class Story
{
    public string? Title { get; set; }
    public string? Uri { get; set; }
    public string? PostyBy{ get; set; }
    public long Time { get; set; }
    public int Score { get; set; }
    public int CommentCount{ get; set; }
    
    public long Id { get; set; }
    public long[]? Kids { get; set; }
    public string? Text { get; set; }
    public string? By { get; set; }
    public int Descendants { get; set; }
    public string? Url { get; set; }
}


