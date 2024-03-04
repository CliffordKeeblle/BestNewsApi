# BestNews WebAPI

## Implementation

Using ASP.NET Core BestNewsApi implements a RESTful API to retrieve the details of the first n "best stories" from the Hacker News API, where n is specified by the caller to the API.
The Hacker News API is documented here: https://github.com/HackerNews/API .
The IDs for the "best stories" can be retrieved from this URI: https://hacker-news.firebaseio.com/v0/beststories.json .
The details for an individual story ID can be retrieved from this URI: https://hacker-news.firebaseio.com/v0/item/21233041.json (in this case for the story with ID
21233041 )

The API returns an array of the first n "best stories" as returned by the Hacker News API, sorted by their score in a descending order, in the form:

	[
	{
	"title": "A uBlock Origin update was rejected from the Chrome Web Store",
	"uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
	"postedBy": "ismaildonmez",
	"time": "2019-10-12T13:43:01+00:00",
	"score": 1716,
	"commentCount": 572
	},
	{ ... },
	{ ... },
	{ ... },
	...
	]

## Endpoints

"BestNews API" and is a .NET 8.0 Solution that hosts a WebAPI providing a single endpoint. This endpoint returns the best N stories from the Hacker News API :

     'http://localhost:5139/api/stories/best?n=[the number of stories to be returned]'
	 e.g. 'http://localhost:5139/api/stories/best?n=10' - returns the best 10 stories in descending score order
	 
## Concurrency

BestNewsApi fetches the best 200 story ids, and then sequentially populates each story. There is an opportunity to improve performance by
coding an implementation that concurrently fetches the Hacker News stories. 

## Caching

ConcurrentDictionary has been used to cache news story. This is not suitable for Production as it does not have an expiry feature.

## Resilince Policy

Microsoft's resilient Nuget package has been used to provide resiliemce and back off strategy. I have nit test this. 

https://devblogs.microsoft.com/dotnet/building-resilient-cloud-services-with-dotnet-8/

## Unit Tests

xUnit, FluentAsserts and FakeItEasy have been used to provide a couple of lightweight unit tests. 

It would be nice to have more productive faking class for httpClient like:
https://github.com/xaviersolau/CodeQuality

## Swagger

A swagger UI is hosted on  http://localhost:5139/index.html .

## Running the BestNews API with Swagger

Clone the C# Solution 

    git clone https://github.com/CliffordKeeblle/BestNewsApi.git

Change to the Solution directory

  cd BestNewsApi/

On the command line type:

   dotnet run
   
Open a web browser and enter http://localhost:5139/swagger/index.html

This will bring up a swagger UI. Select "GET", then "Try Out"

Enter the number of Best Stories to be returned.

Select "Execute" and the results will be returned within a window in the browser.

## Running the BestNews API in CLI

Clone the C# Solution 

    git clone https://github.com/CliffordKeeblle/BestNewsApi.git

Change to the Solution directory

  cd BestNewsAPI

On the command line type:

   dotnet run
   
Open a web browser and enter http://localhost:5139/api/stories/best?n=10 to retrieve the top 10 stories.

