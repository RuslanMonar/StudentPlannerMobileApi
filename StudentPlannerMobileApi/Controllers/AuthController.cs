using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;
using StudentPlanner.Application.Queries;
using StudentPlanner.Domain.Models.Dto;

namespace StudentPlannerMobileApi.Controllers;

public class AuthController : ApiController
{ 
    [HttpPost("SignUp")]
    public async Task<ActionResult<AuthResult>> SignUp(SignUpCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("SignIn")]
    public async Task<ActionResult<AuthResult>> SignIn(SignInQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}