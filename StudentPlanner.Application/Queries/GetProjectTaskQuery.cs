using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Domain.Models.Dto;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Queries;

public class GetProjectTaskQuery : IRequest<List<TasksByProjects>>
{
}

public class GetProjectTaskQueryHandler : IRequestHandler<GetProjectTaskQuery, List<TasksByProjects>>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProjectTaskQueryHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<TasksByProjects>> Handle(GetProjectTaskQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        var result = await _dbContext.Tasks.Where(t => t.UserId == userId).Include(x => x.Project).ToListAsync(cancellationToken);

        var groupedByProjectsData = await _dbContext.Projects.Where(x => x.UserId == userId)
            .Include(x => x.ProjectTasks)
            .Select(x => new TasksByProjects
            {
                Id = x.Id,
                Title = x.Title,
                Color = x.Color,
                FolderId = x.FolderId,
                ProjectTasks = x.ProjectTasks
            })
            .ToListAsync(cancellationToken);

        return groupedByProjectsData;
    }
}