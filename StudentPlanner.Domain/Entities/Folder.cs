namespace StudentPlanner.Domain.Entities;

public class Folder
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
    public Guid UserId { get; set; }
    public List<Project> Projects { get; set; }
}