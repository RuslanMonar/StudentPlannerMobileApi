using System.Text.Json.Serialization;

namespace StudentPlanner.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
    public int FolderId { get; set; }
    public Guid UserId { get; set; }
    public Folder Folder { get; set; }

    [JsonIgnore]
    public List<ProjectTask> ProjectTasks { get; set; }
}