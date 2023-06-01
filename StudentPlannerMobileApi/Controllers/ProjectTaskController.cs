using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;
using StudentPlanner.Application.Queries;
using StudentPlanner.Domain.Models.Dto;

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
    
    [AllowAnonymous]
    [HttpPost("AddTaskTrack")]
    public async Task<IActionResult> EditProjectTasks([FromBody] AddTaskTrackCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }
    

    [HttpGet("GetTaskTrackByMonth")]
    public async Task<ActionResult<TaskTracksByMonthResult>> GetTaskTrackByMonth()
    {
        var result = await Mediator.Send(new GetTaskTracksByMonthQuery());
        return Ok(result);
    }

}