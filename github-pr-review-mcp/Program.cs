using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

var gitAPIKey = builder.Configuration["GitAPIKey"];
var baseUrl = builder.Configuration["BaseUrl"];
var gitOwner = builder.Configuration["GitOwner"];
var repoName = builder.Configuration["RepoName"];

if(string.IsNullOrEmpty(gitAPIKey))
    throw new InvalidOperationException("Github api key is missing.");
if(string.IsNullOrEmpty(baseUrl))
    throw new InvalidOperationException("Github base url is missing.");
if(string.IsNullOrEmpty(gitOwner))
    throw new InvalidOperationException("Github owner name is missing");
if(string.IsNullOrEmpty(repoName))
    throw new InvalidOperationException("Github Repositorie name is missing.");


builder.Services.AddHttpClient();
builder.Services.AddSingleton(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return new GitApiService(httpClientFactory, baseUrl, gitOwner, repoName, gitAPIKey);
});



builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();
// var app = builder.Build();

// var gitService = app.Services.GetRequiredService<GitApiService>();

// var result = await gitService.GetAllPullRequestsAsync("dotnet-codespaces");

// Console.WriteLine(result.ToList());

// // comment this if you only want test run
// return;
