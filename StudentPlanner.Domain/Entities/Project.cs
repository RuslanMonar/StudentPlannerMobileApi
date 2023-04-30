namespace StudentPlanner.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
    public int FolderId { get; set; }
    public Folder Folder { get; set; }

    public List<ProjectTask> ProjectTasks { get; set; }
}