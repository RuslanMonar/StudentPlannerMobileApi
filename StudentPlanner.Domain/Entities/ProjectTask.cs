using System.Text.Json.Serialization;

namespace StudentPlanner.Domain.Entities;

public class ProjectTask
{
    public int Id { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public int TomatoCount { get; set; }
    public int TomatoLength { get; set; }
    public int TotalTime { get; set; }
    public string Flag { get; set; }
    public bool Completed { get; set; }
    public bool Deleted { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public int TimeCompleted { get; set; }
    public int TimeLeft { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public List<TaskTrack> TaskTracks { get; set; }
}