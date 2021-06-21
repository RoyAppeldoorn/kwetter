using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.TimelineService.API.Application.Queries;
using Kwetter.Services.TimelineService.API.Application.Queries.HomeTimelineQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimelineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeTimelineAsync([FromQuery] uint pageNumber, [FromQuery] uint pageSize)
        {
            HttpContext.Request.Headers.TryGetValue("UserId", out var userId);

            HomeTimelineQuery homeTimelineQuery = new()
            {
                UserId = Guid.Parse(userId),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            
            QueryResponse<IEnumerable<KweetDto>> queryResponse = await _mediator.Send(homeTimelineQuery);
            return queryResponse.Success
                ? new OkObjectResult(queryResponse)
                : new BadRequestObjectResult(queryResponse);
        }
    }
}
