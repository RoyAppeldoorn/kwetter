using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Commands.CreateFollowCommand
{
    public record CreateFollowCommand(Guid FollowingId, Guid FollowerId) : IRequest<CommandResult>;
}
