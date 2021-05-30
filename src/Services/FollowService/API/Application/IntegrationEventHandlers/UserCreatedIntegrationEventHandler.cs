using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.IntegrationEventHandlers
{
    public class UserCreatedIntegrationEventHandler : IMessageHandler<UserCreatedIntegrationEvent>
    {
        private readonly IUserRepository _userRepository;

        public UserCreatedIntegrationEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task HandleMessageAsync(string messageType, UserCreatedIntegrationEvent message, CancellationToken cancellationToken)
        {
            User user = new(message.UserId, message.UserName);
            _userRepository.Create(user);
            bool success = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new FollowIntegrationException("Failed to handle UserCreatedIntegrationEvent.");
        }
    }
}
