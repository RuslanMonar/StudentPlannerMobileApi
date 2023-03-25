using StudentPlanner.Application.Interfaces.Infrastructure;

namespace StudentPlanner.Infrastructure.Repositories;

public class UserRepository
{
    private readonly IStudentPlannerContext _studentPlannerContext;

    public UserRepository(IStudentPlannerContext studentPlannerContext)
    {
        _studentPlannerContext = studentPlannerContext;
    }
    
}