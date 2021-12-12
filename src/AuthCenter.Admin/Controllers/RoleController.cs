using AuthCenter.Admin.Application.Command;
using AuthCenter.Admin.Application.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthCenter.Admin.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public RoleController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] RolesQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromForm] RoleCreateCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            return Ok(res);
        }
    }
}
