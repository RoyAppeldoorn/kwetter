using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.KweetService.API.Application.DomainEvents;
using Kwetter.Services.KweetService.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.UnlikeKweet
{
    public class UnlikeKweetCommandHandler : IRequestHandler<UnlikeKweetCommand, CommandResult>
    {
        private readonly IKweetRepository _kweetRepository;
        private readonly IMessagePublisher _messagePublisher;

        public UnlikeKweetCommandHandler(IKweetRepository kweetRepository, IMessagePublisher messagePublisher)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task<CommandResult> Handle(UnlikeKweetCommand request, CancellationToken cancellationToken)
        {
            var kweet = await _kweetRepository.FindAsync(request.KweetId, cancellationToken);

            bool unliked = kweet.RemoveLike(request.UserId);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if(unliked && success)
            {
                // publish domain event to rabbitmq
                KweetUnlikedDomainEvent kweetUnlikedDomainEvent = new(kweetId: kweet.Id, userId: request.UserId);
                await _messagePublisher.PublishMessageAsync("KweetUnliked", kweetUnlikedDomainEvent);
            }

            CommandResult commandResult = new()
            {
                Success = unliked && success,
                Errors = unliked
                    ? new List<string>()
                    : new List<string>() { "The kweet was not liked." }
            };
            return commandResult;
        }
    }
}
