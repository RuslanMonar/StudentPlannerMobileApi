using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;
using StudentPlanner.Application.Queries;

namespace StudentPlannerMobileApi.Controllers;

public class ProjectTaskController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> AddProjectTask([FromBody] AddProjectTaskCommand query)
    {
        await Mediator.Send(query);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectTasks()
    {
        var result = await Mediator.Send(new GetProjectTaskQuery());
        return Ok(result);
    }

    [HttpPost("EditTask")]
    public async Task<IActionResult> EditProjectTasks([FromBody]  EditProjectTaskQuery request)
    {
        await Mediator.Send(request);
        return Ok();
    }
}