using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.KweetService.API.Infrastructure;
using Kwetter.Services.KweetService.API.Infrastructure.Repositories;
using Kwetter.Services.KweetService.API.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.KweetService.API.Application.DomainEvents.KweetCreated;

namespace Kwetter.Services.KweetService.API.Application.Commands.CreateKweet
{
    public class CreateKweetCommandHandler : IRequestHandler<CreateKweetCommand, CommandResult>
    {
        private readonly IKweetRepository _kweetRepository;
        private readonly IMessagePublisher _messagePublisher;

        public CreateKweetCommandHandler(IKweetRepository kweetRepository, IMessagePublisher messagePublisher)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task<CommandResult> Handle(CreateKweetCommand request, CancellationToken cancellationToken)
        {
            Kweet kweet = new(request.KweetId, request.UserId, request.Message);
            Kweet trackedKweet = _kweetRepository.Create(kweet);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if(success)
            {
                // publish domain event to rabbitmq
                KweetCreatedDomainEvent kweetCreatedDomainEvent = new(kweetId: kweet.Id, userId: kweet.UserId, message: kweet.Message, createdDateTime: kweet.CreatedDateTime);
                await _messagePublisher.PublishMessageAsync("KweetCreated", kweetCreatedDomainEvent);
            }

            return new CommandResult { 
                Success = trackedKweet != null && success 
            };
        }
    }
}
