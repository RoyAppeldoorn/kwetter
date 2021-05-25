using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.KweetService.API.Application.Commands.CreateKweet;
using Kwetter.Services.KweetService.API.Application.Commands.LikeKweet;
using Kwetter.Services.KweetService.API.Application.Commands.UnlikeKweet;
using Kwetter.Services.KweetService.API.Application.Queries.GetKweetsByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KweetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KweetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateKweetCommand command)
        {
            HttpContext.Request.Headers.TryGetValue("UserId", out var userId);
            if (command.UserId != Guid.Parse(userId))
                return UnauthorizedCommand();
            
            CommandResult commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? new CreatedAtRouteResult(new { Id = command.KweetId }, commandResult)
                : BadRequest(commandResult);
        }

        [HttpPost("like")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LikeAsync(LikeKweetCommand command)
        {
            CommandResult commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? new OkObjectResult(commandResult)
                : BadRequest(commandResult);
        }

        [HttpDelete("like")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnlikeAsync(UnlikeKweetCommand command)
        {
            CommandResult commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? new OkObjectResult(commandResult)
                : BadRequest(commandResult);
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetKweetsByUserIdAsync(Guid UserId)
        {
            var result = await _mediator.Send(new GetKweetsByUserIdQuery(UserId));
            return result != null ? Ok(result) : BadRequest();
        }

        private IActionResult UnauthorizedCommand()
        {
            return Unauthorized(new CommandResult()
            {
                Errors = new List<string>() { "The user id claim does not match the provided user id" },
                Success = false
            });
        }
    }
}
