using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StudentPlannerMobileApi.Controllers;

[ApiController]
[Route("api/studentplanner/[controller]")]
public class ApiController : ControllerBase
{
#pragma warning disable CS8618
    private IMediator _mediator;
#pragma warning restore CS8618

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;
}