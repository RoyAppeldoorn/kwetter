using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.KweetService.API.DataAccess.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
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

            bool liked = kweet.AddLike(request.UserId);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            CommandResult commandResult = new()
            {
                Success = liked && success,
                Errors = liked
                    ? new List<string>()
                    : new List<string>() { "The kweet is already liked." }
            };
            return commandResult;
        }
    }
}
