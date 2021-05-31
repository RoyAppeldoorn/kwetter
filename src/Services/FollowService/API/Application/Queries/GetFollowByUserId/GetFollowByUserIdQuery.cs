using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Queries.GetFollowByUserId
{
    public record GetFollowByUserIdQuery : IRequest<QueryResponse<UserDto>>
    {
        public Guid UserId { get; set; }
    }
}
