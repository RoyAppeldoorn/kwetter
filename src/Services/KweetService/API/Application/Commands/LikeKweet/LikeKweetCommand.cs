using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.LikeKweet
{
    public record LikeKweetCommand : IRequest<CommandResult>
    {
        public Guid UserId { get; init; }

        public Guid KweetId { get; init; }
    }
}
