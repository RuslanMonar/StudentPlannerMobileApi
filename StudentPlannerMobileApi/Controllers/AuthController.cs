using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;
using StudentPlanner.Domain.Models.Dto;

namespace StudentPlannerMobileApi.Controllers;

public class AuthController : ApiController
{ 
    [HttpPost]
    public async Task<ActionResult<AuthResult>> SignUp(SignUpCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}