using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.FollowService.API.Application.DomainEvents.UserUnfollowed;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Commands.DeleteFollowCommand
{
    public class DeleteFollowCommandHandler : IRequestHandler<DeleteFollowCommand, CommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessagePublisher _messagePublisher;

        public DeleteFollowCommandHandler(IUserRepository userRepository, IMessagePublisher messagePublisher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task<CommandResult> Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(request.FollowerId, cancellationToken);
            User otherUser = await _userRepository.FindByIdAsync(request.FollowingId, cancellationToken);
            bool unfollowed = user.Unfollow(otherUser);
            bool success = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if(success)
            {
                // publish domain event to rabbitmq
                UserUnfollowedDomainEvent userUnfollowedDomainEvent = new(followingId: request.FollowingId, followerId: request.FollowerId);
                await _messagePublisher.PublishMessageAsync("UserUnfollowed", userUnfollowedDomainEvent);
            }

            return new CommandResult()
            {
                Success = unfollowed && success
            };
        }
    }
}
