using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;

namespace StudentPlannerMobileApi.Controllers;

public class ProjectsController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> AddProject([FromBody] AddProjectCommand query)
    {
        var projectId = await Mediator.Send(query);
        return Ok(projectId);
    }
}