using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.FollowService.API.Application.Commands.CreateFollowCommand;
using Kwetter.Services.FollowService.API.Application.Commands.DeleteFollowCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : Controller
    {
        private readonly IMediator _mediator;

        public FollowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateFollowCommand command)
        {
            HttpContext.Request.Headers.TryGetValue("UserId", out var userId);
            if (command.FollowerId != Guid.Parse(userId))
                return UnauthorizedCommand();

            CommandResult commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? new CreatedAtRouteResult(new { command.FollowingId, command.FollowerId }, commandResult)
                : BadRequest(commandResult);
        }

        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(DeleteFollowCommand command)
        {
            HttpContext.Request.Headers.TryGetValue("UserId", out var userId);
            if (command.FollowerId != Guid.Parse(userId))
                return UnauthorizedCommand();

            CommandResult commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? new OkObjectResult(commandResult)
                : BadRequest(commandResult);
        }

        private IActionResult UnauthorizedCommand()
        {
            return Unauthorized(new CommandResult()
            {
                Errors = new List<string>() { "The user id and follower id are not the same." },
                Success = false
            });
        }
    }
}
