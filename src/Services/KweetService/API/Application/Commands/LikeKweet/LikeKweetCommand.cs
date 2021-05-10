using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.LikeKweet
{
    public record LikeKweetCommand(Guid UserId, Guid KweetId) : IRequest<CommandResult>;
}
