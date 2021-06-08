using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.UserService.API.Application.DomainEvents;
using Kwetter.Services.UserService.API.Domain;
using Kwetter.Services.UserService.API.Infrastructure.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application.IntegrationEventHandlers.IdentityCreated
{
    public class IdentityCreatedIntegrationEventHandler : IMessageHandler<IdentityCreatedIntegrationEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessagePublisher _messagePublisher;

        public IdentityCreatedIntegrationEventHandler(IUserRepository userRepository, IMessagePublisher messagePublisher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task HandleMessageAsync(string messageType, IdentityCreatedIntegrationEvent message, CancellationToken cancellationToken = default)
        {
            User user = await _userRepository.FindByIdAsync(message.UserId, cancellationToken);
            
            if (user != default)
                throw new UserIntegrationException("A user with the proposed user id already exists.");
            user = await _userRepository.FindByUserNameAsync(message.UserName, cancellationToken);
            if (user != default)
                throw new UserIntegrationException("A user with the proposed user name already exists.");
           
            user = new(message.UserId, message.UserName);
            _userRepository.Create(user);
            bool success = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new UserIntegrationException("Failed to handle IdentityCreatedIntegrationEvent.");

            // publish UserCreatedDomainEvent event to rabbitmq
            UserCreatedDomainEvent userCreatedDomainEvent = new(user.Id, user.Username);
            await _messagePublisher.PublishMessageAsync("UserCreated", userCreatedDomainEvent);
        }
    }
}
