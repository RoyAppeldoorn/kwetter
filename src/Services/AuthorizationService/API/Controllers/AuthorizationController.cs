using Kwetter.Services.AuthorizationService.API.Application.Commands.SetCustomClaims;
using Kwetter.Services.Common.API.CQRS;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("Claims")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetCustomClaimsAsync([FromBody] SetCustomClaimsCommand claimsCommand)
        {
            CommandResult commandResult = await _mediator.Send(claimsCommand);
            return commandResult.Success
                ? new OkObjectResult(commandResult)
                : new BadRequestObjectResult(commandResult);
        }
    }
}
