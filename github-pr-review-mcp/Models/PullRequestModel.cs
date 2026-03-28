
using System.Text.Json.Serialization;


public class PullRequest
{
    public long Id { get; set; }
    public string NodeId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string State { get; set; }
    public bool Locked { get; set; }
    public string Body { get; set; }
    public string HtmlUrl { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public DateTime? MergedAt { get; set; }

    public GitHubUser User { get; set; }
    public List<GitHubLabel> Labels { get; set; } = new();
    public GitHubBranch Head { get; set; }
    public GitHubBranch Base { get; set; }
}

public class GitHubUser
{
    public long Id { get; set; }
    public string Login { get; set; }
    public string HtmlUrl { get; set; }
    public bool SiteAdmin { get; set; }
}

public class GitHubLabel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
}

public class GitHubBranch
{
    public string Ref { get; set; }
    public string Sha { get; set; }
    public GitHubUser User { get; set; }
}


public class PullRequestFile
{
    public string Sha { get; set; }

    [JsonPropertyName("filename")]
    public string FileName { get; set; }

    public string Status { get; set; }
    public int Additions { get; set; }
    public int Deletions { get; set; }
    public int Changes { get; set; }

    [JsonPropertyName("blob_url")]
    public string BlobUrl { get; set; }

    [JsonPropertyName("raw_url")]
    public string RawUrl { get; set; }

    [JsonPropertyName("contents_url")]
    public string ContentsUrl { get; set; }

    public string Patch { get; set; }
}