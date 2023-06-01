using StudentPlanner.Domain.Models.Dto;

namespace StudentPlanner.Domain.Entities;

public class TaskTrack
{
    public int Id { get; set; }
    public int ProjectTaskId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TimeSpentInMinutes { get; set; }

    public ProjectTask ProjectTask { get; set; }
}