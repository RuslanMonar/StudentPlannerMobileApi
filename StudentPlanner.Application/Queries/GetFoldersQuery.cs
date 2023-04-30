using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Queries;

public class GetFoldersQuery : IRequest<List<Folder>>
{
}

public class GetFoldersRequestHandler : IRequestHandler<GetFoldersQuery, List<Folder>>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetFoldersRequestHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Folder>> Handle(GetFoldersQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        var folders = await _dbContext.Folders.Where(f => f.UserId == userId)
            .ToListAsync(cancellationToken: cancellationToken);
        return folders;
    }
}