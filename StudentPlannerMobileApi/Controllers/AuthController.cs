using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Application.Commands;

namespace StudentPlannerMobileApi.Controllers;

public class AuthController : ApiController
{ 
    [HttpPost]
    public async Task<ActionResult> SignUp(SignUpCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok();
    }
}