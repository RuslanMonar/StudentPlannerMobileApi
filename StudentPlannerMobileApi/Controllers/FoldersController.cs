using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;
using StudentPlanner.Application.Queries;
using StudentPlanner.Domain.Entities;

namespace StudentPlannerMobileApi.Controllers;

public class FoldersController : ApiController
{
    [HttpPost]
    public async Task<ActionResult<Folder>> CreateFolder(AddFolderCommand request)
    {
        var folder = await Mediator.Send(request);
        return Ok(folder);
    }

    [HttpGet]
    public async Task<ActionResult<List<Folder>>> GetAllFolders()
    {
        var request = new GetFoldersQuery();
        var folders = await Mediator.Send(request);
        return Ok(folders);
    }
}