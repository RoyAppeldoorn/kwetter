using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.FollowService.API.Application.DomainEventHandlers.UserFollowed;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Commands.CreateFollowCommand
{
    public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, CommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessagePublisher _messagePublisher;

        public CreateFollowCommandHandler(IUserRepository userRepository, IMessagePublisher messagePublisher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task<CommandResult> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(request.FollowerId, cancellationToken);
            User otherUser = await _userRepository.FindByIdAsync(request.FollowingId, cancellationToken);
            bool followed = user.Follow(otherUser);
            bool success = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (success)
            {
                // publish domain event to rabbitmq
                UserFollowedDomainEvent userFollowedDomainEvent = new(followingId: request.FollowingId, followerId: request.FollowerId);
                await _messagePublisher.PublishMessageAsync("UserFollowed", userFollowedDomainEvent);
            }
            
            return new CommandResult()
            {
                Success = followed && success
            };
        }
    }
}
