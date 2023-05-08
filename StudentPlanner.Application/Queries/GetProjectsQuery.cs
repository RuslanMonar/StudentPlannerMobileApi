using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Queries;

public class GetProjectsQuery : IRequest<Project[]>
{
}

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, Project[]>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProjectsQueryHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Project[]> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        var result = await _dbContext.Projects.Where(x => x.UserId == userId).Select(x => x).ToArrayAsync(cancellationToken);

        return result;
    }
}