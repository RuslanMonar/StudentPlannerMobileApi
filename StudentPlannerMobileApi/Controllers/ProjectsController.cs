using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;
using StudentPlanner.Application.Queries;
using StudentPlanner.Domain.Entities;

namespace StudentPlannerMobileApi.Controllers;

public class ProjectsController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> AddProject([FromBody] AddProjectCommand query)
    {
        var projectId = await Mediator.Send(query);
        return Ok(projectId);
    }

    [HttpGet]
    public async Task<ActionResult<Project[]>> GetProjects()
    {
        var result = await Mediator.Send(new GetProjectsQuery());
        return Ok(result);
    }
}