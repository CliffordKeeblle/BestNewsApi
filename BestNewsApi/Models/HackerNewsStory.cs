﻿namespace BestNewsApi.Models;

// The Hacker News API is documented here: https://github.com/HackerNews/API .

public class HackerNewsStory
{
    public long Id { get; set; }
    public bool Deleted { get; set; }
    public string? Type { get; set; }
    public string? By { get; set; }
    public string? Title { get; set; }
    public long Time { get; set; }
    public string? Text { get; set; }
    public long[]? Kids { get; set; }
    public string? Url { get; set; }
    public int Score { get; set; }  
    public int Descendants { get; set; }
}


