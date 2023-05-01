using MediatR;
using Microsoft.AspNetCore.Http;
using StudentPlanner.Application.Interfaces.Infrastructure;
using StudentPlanner.Domain.Entities;
using StudentPlanner.Shared.Extensions;

namespace StudentPlanner.Application.Commands;

public class AddProjectCommand : IRequest<Unit>
{
    public string Title { get; set; }
    public string Color { get; set; }
    public int FolderId { get; set; }
}

public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, Unit>
{
    private readonly IStudentPlannerContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddProjectCommandHandler(IStudentPlannerContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        var project = new Project
        {
            Title = request.Title,
            Color = request.Color,
            FolderId = request.FolderId,
            UserId = userId
        };

        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync(cancellationToken);


        return Unit.Value;
    }
}