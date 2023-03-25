using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain;

namespace StudentPlanner.Infrastructure.Data;

public class StudentPlannerContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IStudentPlannerContext
{
    public StudentPlannerContext(DbContextOptions<StudentPlannerContext> options) : base(options)
    {
    }
}