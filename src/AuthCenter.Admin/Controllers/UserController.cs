using AuthCenter.Admin.Application.Command;
using AuthCenter.Admin.Application.Queries.Users;
using AuthCenter.Domain.Entities;
using EatMeat.Core.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthCenter.Admin.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList([FromQuery] UsersQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UserCreateCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            if (res.Id != null)
            {
                return Created(Request.Host.Value + res.Id, res);
            }
            else
            {
                return BadRequest(res.Errs);
            }
        }
    }
}
