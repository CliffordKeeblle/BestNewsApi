using BestNewsApi.Interfaces;
using BestNewsApi.Services;
using Microsoft.Extensions.Http.Resilience;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IStoryService, StoryService>();
builder.Services.AddSingleton<IArticleService, ArticleService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("BestNewApi")
        .AddResilienceHandler("my-pipeline", builder =>
         {
             // Refer to https://www.pollydocs.org/strategies/retry.html#defaults for retry defaults
             builder.AddRetry(new HttpRetryStrategyOptions
             {
                 MaxRetryAttempts = 4,
                 Delay = TimeSpan.FromSeconds(2),
                 BackoffType = DelayBackoffType.Exponential
             });

             // Refer to https://www.pollydocs.org/strategies/timeout.html#defaults for timeout defaults
             builder.AddTimeout(TimeSpan.FromSeconds(5));
         });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
