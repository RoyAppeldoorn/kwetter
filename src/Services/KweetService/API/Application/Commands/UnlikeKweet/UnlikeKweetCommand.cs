using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.UnlikeKweet
{
    public record UnlikeKweetCommand(Guid UserId, Guid KweetId) : IRequest<CommandResult>;
}
