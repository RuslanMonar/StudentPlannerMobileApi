using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Domain.Models.Dto;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Queries;

public class GetFoldersQuery : IRequest<List<FolderWithProjectsDto>>
{
}

public class GetFoldersRequestHandler : IRequestHandler<GetFoldersQuery, List<FolderWithProjectsDto>>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetFoldersRequestHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<FolderWithProjectsDto>> Handle(GetFoldersQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        var folders = await _dbContext.Folders
            .Include(f => f.Projects)
            .Select(f => new FolderWithProjectsDto
            {
                Id = f.Id,
                Title = f.Title,
                Color = f.Color,
                Projects = f.Projects.Select(p => new Project
                {
                    Id = p.Id,
                    Title = p.Title,
                    Color = p.Color
                }).ToList()
            })
            .ToListAsync(cancellationToken);


        return folders;
    }
}