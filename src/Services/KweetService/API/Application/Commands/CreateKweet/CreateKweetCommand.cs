using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;

namespace Kwetter.Services.KweetService.API.Application.Commands.CreateKweet
{
    public record CreateKweetCommand(Guid KweetId, Guid UserId, string Message): IRequest<CommandResult>;
}
