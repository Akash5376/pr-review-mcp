using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;


[McpServerToolType, Description("Tool for fetching GitHub pull request details")]
public class GitMCPTool
{

    [McpServerTool, Description("Fetch the list of repositories from GitHub for the specified owner.")]
    public static async Task<List<Repository>> GetRepositoryListAsync(GitApiService gitApiService)
    {
        var repositories = await gitApiService.GetRepositoryListAsync();
        return repositories;
    }


    [McpServerTool, Description("Fetch all pull requests from the GitHub repository.")]
    public static async Task<List<PullRequest>> GetAllPullRequestsAsync( GitApiService gitApiService,
        [Description("The name of the repository to fetch pull requests from repository. Repository name derived from the GetRepositoryListAsync tool.")] string repoName)
    {
        var pullRequests = await gitApiService.GetAllPullRequestsAsync(repoName);
        return pullRequests;
    }

    [McpServerTool, Description("This tool will fetch the Pull Request(PR) details based on the PR number(PR Number can be obtained from GetAllPullRequests tool). For example when User ask for get me the My PR changes then this tool will fetch the details of that specific PR. Show the list of files changed in that PR, additions and deletions in each file.")]
    public static async Task<List<PullRequestFile>> GetPullRequestDetails(GitApiService gitApiService, 
    [Description("The pull request number, derived from the GetAllPullRequests tool")]int pullRequestNumber)
    {
        var pullRequestDetails = await gitApiService.GetPullRequestDetailsAsync(pullRequestNumber);
        return pullRequestDetails;
    }

    
}
