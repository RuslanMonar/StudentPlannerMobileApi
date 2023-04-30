using System.Text.Json.Serialization;

namespace StudentPlanner.Domain.Entities;

public class Folder
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public List<Project> Projects { get; set; }
}