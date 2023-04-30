using MediatR;
using Microsoft.AspNetCore.Http;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Commands;

public class AddFolderCommand : IRequest<Folder>
{
    public string Title { get; set; }
    public string Color { get; set; }
}
public class CreateFolderRequestHandler : IRequestHandler<AddFolderCommand, Folder>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateFolderRequestHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Folder> Handle(AddFolderCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        var folder = new Folder
        {
            Title = request.Title,
            Color = request.Color,
            UserId = userId
        };

        _dbContext.Folders.Add(folder);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return folder;
    }
}
