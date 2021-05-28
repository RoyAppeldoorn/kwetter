using Kwetter.Services.Common.Infrastructure.Messaging;
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

        public IdentityCreatedIntegrationEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task HandleMessageAsync(string messageType, IdentityCreatedIntegrationEvent notification, CancellationToken cancellationToken = default)
        {
            User user = await _userRepository.FindByIdAsync(notification.UserId, cancellationToken);
            if (user != default)
                throw new UserIntegrationException("A user with the proposed user id already exists.");
            user = await _userRepository.FindByUserNameAsync(notification.UserName, cancellationToken);
            if (user != default)
                throw new UserIntegrationException("A user with the proposed user name already exists.");

            user = new(notification.UserId, notification.UserName);
            _userRepository.Create(user);

            bool success = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new UserIntegrationException("Failed to handle IdentityCreatedIntegrationEvent.");
        }
    }
}
