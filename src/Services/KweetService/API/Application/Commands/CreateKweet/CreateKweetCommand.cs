using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;

namespace Kwetter.Services.KweetService.API.Application.Commands.CreateKweet
{
    public class CreateKweetCommand: IRequest<CommandResult>
    {
        public Guid KweetId { get; init; }

        public Guid UserId { get; init; }

        public string Message { get; init; }
    }
}
