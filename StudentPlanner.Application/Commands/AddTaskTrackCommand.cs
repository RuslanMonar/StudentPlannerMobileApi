using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Commands;

public class AddTaskTrackCommand : IRequest<Unit>
{
    public int TaskId{ get; set; }
    public DateTime StartDate{ get; set; }
    public int TimeSpentInMinutes { get; set; }
}

public class AddTaskTrackCommandHandler : IRequestHandler<AddTaskTrackCommand, Unit>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddTaskTrackCommandHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(AddTaskTrackCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();
        //var userId = new Guid("9542147e-4353-4a17-a172-15b34c60f9c0");

        var taskTrack = new TaskTrack
        {
            ProjectTaskId = request.TaskId,
            StartDate = request.StartDate,
            EndDate = request.StartDate.AddMinutes(request.TimeSpentInMinutes),
            TimeSpentInMinutes = request.TimeSpentInMinutes
        };

        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == request.TaskId, cancellationToken: cancellationToken);

        if (request.TimeSpentInMinutes != 0)
        {
            task.TimeCompleted += request.TimeSpentInMinutes;
        }

        _dbContext.TaskTracks.Add(taskTrack);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}