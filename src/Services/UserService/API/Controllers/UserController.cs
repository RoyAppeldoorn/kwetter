using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.UserService.API.Application.Queries.GetProfileByName;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProfileByNameAsync(string username)
        {
            GetProfileByNameQuery getProfileByNameQuery = new()
            {
                Username = username
            };

            QueryResponse<ProfileDto> queryResponse = await _mediator.Send(getProfileByNameQuery);
            return queryResponse.Success
                ? new OkObjectResult(queryResponse)
                : new BadRequestObjectResult(queryResponse);
        }
    }
}
