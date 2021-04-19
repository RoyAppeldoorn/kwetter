using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.KweetService.API.Application.Commands.CreateKweet;
using Kwetter.Services.KweetService.API.Application.Queries.GetKweetsByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            CommandResult commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? new CreatedAtRouteResult(new { Id = command.KweetId }, commandResult)
                : BadRequest(commandResult);
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetKweetsByUserId(Guid UserId)
        {
            var result = await _mediator.Send(new GetKweetsByUserIdQuery(UserId));
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
