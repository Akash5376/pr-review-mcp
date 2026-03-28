using System.Text;
using System.Text.Json;


public class GitApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseUrl;
    private readonly string _gitOwner;
     private readonly string _repoName;
    private readonly string _gitAPIKey;
   

    public GitApiService(
        IHttpClientFactory httpClientFactory,
        string baseUrl,
        string gitOwner,
        string repoName,
        string gitAPIKey
        

        )
    {
        _httpClientFactory = httpClientFactory;
        _baseUrl = baseUrl.TrimEnd('/');
        _gitOwner = gitOwner;
         _repoName = repoName;
        _gitAPIKey = gitAPIKey;
       
    }

    public async Task<List<Repository>> GetRepositoryListAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _gitAPIKey);

            client.DefaultRequestHeaders.UserAgent.ParseAdd("github-pr-review-mcp");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            var url = $"{_baseUrl}/users/{_gitOwner}/repos";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<List<Repository>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result ?? new List<Repository>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching repository list: {ex.Message}");
            return new List<Repository>();
        }
    }


    public async Task<List<PullRequest>> GetAllPullRequestsAsync(string repoName)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _gitAPIKey);

            client.DefaultRequestHeaders.UserAgent.ParseAdd("github-pr-review-mcp");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github.full+json");
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            var url = $"{_baseUrl}/repos/{_gitOwner}/{repoName}/pulls";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response);    
            var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<PullRequest>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result ?? new List<PullRequest>();           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching pull requests: {ex.Message}");
            return new List<PullRequest>();
        }
    }

    public async Task<List<PullRequestFile>> GetPullRequestDetailsAsync(int pullRequestNumber)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _gitAPIKey);

            client.DefaultRequestHeaders.UserAgent.ParseAdd("github-pr-review-mcp");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            var url = $"{_baseUrl}/repos/{_gitOwner}/{_repoName}/pulls/{pullRequestNumber}/files";

             var response = await client.GetAsync(url);
                 response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<PullRequestFile>>(stream,
                new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<PullRequestFile>();
        }
        catch (Exception ex)
        {
           Console.WriteLine($"Error fetching pull request details: {ex.Message}");
           return new List<PullRequestFile>();
        }


    }

}