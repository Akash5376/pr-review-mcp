public class Repository
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public bool Private { get; set; }
    public string HtmlUrl { get; set; }
    public string Description { get; set; }
    public string DefaultBranch { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Owner Owner { get; set; }
}

public class Owner
{
    public string Login { get; set; }
    public int Id { get; set; }
    public string HtmlUrl { get; set; }
}