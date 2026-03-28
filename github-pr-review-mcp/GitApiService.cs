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

    public async Task<string> GetRepositoryListAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _gitAPIKey);

            client.DefaultRequestHeaders.UserAgent.ParseAdd("github-pr-review-mcp");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            var url = $"{_baseUrl}/orgs/{_gitOwner}/repos";

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            return body;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching repository list: {ex.Message}");
            return $"Error fetching repository list: {ex.Message}";
        }
    }


    public async Task<string> GetAllPullRequestsAsync()
{
    try
    {
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _gitAPIKey);

        client.DefaultRequestHeaders.UserAgent.ParseAdd("github-pr-review-mcp");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github.full+json");
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

        var url = $"{_baseUrl}/repos/{_gitOwner}/{_repoName}/pulls";

        var response = await client.GetAsync(url);
        var body = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        return body;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching pull requests: {ex.Message}");
        return $"Error fetching pull requests: {ex}";
    }
}

    public async Task<string> GetPullRequestDetailsAsync(int pullRequestNumber)
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
             var body = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            return body;
        }
        catch (Exception ex)
        {
           Console.WriteLine($"Error fetching pull request details: {ex.Message}");
           return $"Error fetching pull request details: {ex.Message}";
        }


    }

}