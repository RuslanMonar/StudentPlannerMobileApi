using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Commands;

public class EditProjectTaskQuery : IRequest
{
    public int TaskId { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public int TomatoCount { get; set; }
    public string Flag { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}

public class EditProjectTaskQueryHandler : IRequestHandler<EditProjectTaskQuery>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EditProjectTaskQueryHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Handle(EditProjectTaskQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == request.TaskId, cancellationToken: cancellationToken);

        if (task == null)
        {
            throw new Exception("Task not found");
        }

        task.Title = request.Title;
        task.TomatoCount = request.TomatoCount;
        task.Flag = request.Flag;
        task.Date = request.Date;
        task.Description = request.Description;
        task.ProjectId = request.ProjectId;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}