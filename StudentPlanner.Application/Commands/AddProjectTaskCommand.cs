using MediatR;
using Microsoft.AspNetCore.Http;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Commands;

public class AddProjectTaskCommand : IRequest<Unit>
{
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public int TomatoCount { get; set; } = 1;
    public int TomatoLength { get; set; } = 25;
    public string Flag { get; set; }
    public DateTime Date { get; set; }
}

public class AddProjectTaskCommandHandler : IRequestHandler<AddProjectTaskCommand, Unit>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddProjectTaskCommandHandler(IHttpContextAccessor httpContextAccessor, IStudentPlannerContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();
        var project = await _dbContext.Projects.FindAsync(request.ProjectId);

        if (project == null)
        {
            throw new Exception("Project not Found");
        }

        var projectTask = new ProjectTask
        {
            UserId = userId,
            Title = request.Title,
            TomatoCount = request.TomatoCount,
            TomatoLength = request.TomatoLength,
            TotalTime = request.TomatoCount * request.TomatoLength,
            Flag = request.Flag,
            Date = request.Date,
            TimeCompleted = 0,
            TimeLeft = request.TomatoCount * request.TomatoLength,
            ProjectId = request.ProjectId,
        };

        _dbContext.Tasks.Add(projectTask);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}