using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain;
using StudentPlanner.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace StudentPlanner.Infrastructure.Data;

public class StudentPlannerContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IStudentPlannerContext
{
    public DbSet<Folder> Folders { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }

    public StudentPlannerContext(DbContextOptions<StudentPlannerContext> options) : base(options)
    {
    }
}