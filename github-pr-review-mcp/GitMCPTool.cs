using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;


[McpServerToolType, Description("Tool for fetching GitHub pull request details")]
public class GitMCPTool
{

    [McpServerTool, Description("Fetch all pull requests from the GitHub repository.")]
    public static async Task<string> GetAllPullRequests(GitApiService gitApiService)
    {
        var pullRequestsJson = await gitApiService.GetAllPullRequestsAsync();
        return JsonSerializer.Serialize(pullRequestsJson);
    }

    [McpServerTool, Description("This tool will fetch the Pull Request(PR) details based on the PR number(PR Number can be obtained from GetAllPullRequests tool). For example when User ask for get me the My PR changes then this tool will fetch the details of that specific PR.")]
    public static async Task<string> GetPullRequestDetails(GitApiService gitApiService, 
    [Description("The pull request number, derived from the GetAllPullRequests tool")]int pullRequestNumber)
    {
        var pullRequestDetailsJson = await gitApiService.GetPullRequestDetailsAsync(pullRequestNumber);
        return JsonSerializer.Serialize(pullRequestDetailsJson);
    }

    [McpServerTool, Description("Fetch the list of repositories from GitHub for the specified owner.")]
    public static async Task<string> GetRepositoryListAsync(GitApiService gitApiService)
    {
        var repositoryListJson = await gitApiService.GetRepositoryListAsync();
        return JsonSerializer.Serialize(repositoryListJson);
    }
}
