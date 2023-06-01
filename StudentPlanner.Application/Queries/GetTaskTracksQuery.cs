using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Models.Dto;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Queries;

public class GetTaskTracksByMonthQuery : IRequest<TaskTracksByMonthResult>
{
}

public class GetTaskTracksRequestHandler : IRequestHandler<GetTaskTracksByMonthQuery, TaskTracksByMonthResult>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetTaskTracksRequestHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<TaskTracksByMonthResult> Handle(GetTaskTracksByMonthQuery request, CancellationToken cancellationToken)
    {
        //var userId = _httpContextAccessor.HttpContext!.GetUserId();



        var taskTracksByMonth = await _dbContext.TaskTracks
            .GroupBy(tt => new { tt.StartDate.Year, tt.StartDate.Month })
            .Select(g => new
            {
                MonthIndex = g.Key.Year * 12 + g.Key.Month,
                TotalTimeSpent = g.Sum(tt => tt.TimeSpentInMinutes) / 60.0
            })
            .OrderBy(g => g.MonthIndex)
            .ToListAsync(cancellationToken);

        var months = taskTracksByMonth.Select(result => GetMonthName(result.MonthIndex)).ToList();
        var hours = taskTracksByMonth.Select(result => result.TotalTimeSpent).ToList();

        var projectTasks = _dbContext.Tasks.Include(pt => pt.TaskTracks);

        var projectTaskList = projectTasks.Select(pt => new
        {
            ProjectTask = pt.Title,
            TimeSpentInMinutes = pt.TaskTracks.Sum(tt => tt.TimeSpentInMinutes)
        }).ToList();

        var projectTaskNames = projectTaskList.Select(pt => pt.ProjectTask).ToList();
        var projectTaskTime = projectTaskList.Select(pt => pt.TimeSpentInMinutes / 60.0).ToList();

        var result = new TaskTracksByMonthResult
        {
            Monthes = months,
            SpentMonthHours = hours,
            ProjectTaskNames = projectTaskNames,
            ProjectTaskTime = projectTaskTime
        };

        return result;
    }

    private static string GetMonthName(int monthIndex)
    {
        DateTimeFormatInfo englishDateTimeFormat = new DateTimeFormatInfo
        {
            MonthNames = new[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }
        };
        return englishDateTimeFormat.GetMonthName((monthIndex - 1) % 12 + 1);
    }
}