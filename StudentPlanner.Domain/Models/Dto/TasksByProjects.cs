using StudentPlanner.Domain.Entities;

namespace StudentPlanner.Domain.Models.Dto;

public class TasksByProjects
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
    public int FolderId { get; set; }

    public List<ProjectTask> ProjectTasks { get; set; }
}