using Microsoft.EntityFrameworkCore;
using StudentPlanner.Domain.Entities;

namespace StudentPlanner.Application.Interfaces.Infrastructure;

public interface IStudentPlannerContext
{
    public DbSet<Folder> Folders { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}