using StudentPlanner.Domain.Entities;

namespace StudentPlanner.Domain.Models.Dto;

public class FolderWithProjectsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
    public List<Project> Projects { get; set; }
}