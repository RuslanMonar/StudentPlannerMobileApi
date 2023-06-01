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
    public int TimeCompleted { get; set; } = 0;
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
       // var userId = _httpContextAccessor.HttpContext!.GetUserId();
       var userId = new Guid("9542147e-4353-4a17-a172-15b34c60f9c0");

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

        if (request.TimeCompleted != 0)
        {
            task.TimeCompleted += request.TimeCompleted;
        } 

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}