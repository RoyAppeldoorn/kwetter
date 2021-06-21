using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.KweetService.API.Application.DomainEvents.KweetLiked;
using Kwetter.Services.KweetService.API.Infrastructure.Repositories;
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
        private readonly IMessagePublisher _messagePublisher;

        public LikeKweetCommandHandler(IKweetRepository kweetRepository, IMessagePublisher messagePublisher)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task<CommandResult> Handle(LikeKweetCommand request, CancellationToken cancellationToken)
        {
            var kweet = await _kweetRepository.FindAsync(request.KweetId, cancellationToken);

            bool liked = kweet.AddLike(request.UserId);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if(liked && success)
            {
                // publish domain event to rabbitmq
                KweetLikedDomainEvent kweetLikedDomainEvent = new(kweetId: kweet.Id, kweetUserId: kweet.UserId, userId: request.UserId);
                await _messagePublisher.PublishMessageAsync("KweetLiked", kweetLikedDomainEvent);
            }

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
