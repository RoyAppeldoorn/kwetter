using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.KweetService.API.DataAccess;
using Kwetter.Services.KweetService.API.DataAccess.Repositories;
using Kwetter.Services.KweetService.API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.LikeKweet
{
    public class LikeKweetCommandHandler : IRequestHandler<LikeKweetCommand, CommandResult>
    {
        private readonly IKweetRepository _kweetRepository;

        public LikeKweetCommandHandler(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
        }

        public async Task<CommandResult> Handle(LikeKweetCommand request, CancellationToken cancellationToken)
        {
            var kweet = await _kweetRepository.FindAsync(request.KweetId, cancellationToken);
            bool liked = kweet.Likes.Any(x => x.UserId == request.UserId);

            if (liked)
            {
                CommandResult error = new()
                {
                    Success = false,
                    Errors = new List<string>() { "The kweet is already liked." }
                };
                return error;
            }

            // Add like to the kweet and update
            KweetLike like = new(request.KweetId, request.UserId);
            kweet.Likes.Add(like);

            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            CommandResult commandResult = new()
            {
                Success = success,
            };
            return commandResult;
        }
    }
}
