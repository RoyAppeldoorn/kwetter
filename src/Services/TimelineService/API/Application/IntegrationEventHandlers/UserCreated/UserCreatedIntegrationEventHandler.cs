using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.TimelineService.API.Domain;
using Kwetter.Services.TimelineService.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserCreated
{
    public class UserCreatedIntegrationEventHandler : IMessageHandler<UserCreatedIntegrationEvent>
    {
        private readonly ITimelineGraphRepository _timelineGraphRepository;

        public UserCreatedIntegrationEventHandler(ITimelineGraphRepository timelineGraphRepository)
        {
            _timelineGraphRepository = timelineGraphRepository ?? throw new ArgumentNullException(nameof(timelineGraphRepository));
        }

        public async Task HandleMessageAsync(string messageType, UserCreatedIntegrationEvent message, CancellationToken cancellationToken)
        {
            User user = new()
            {
                Id = message.UserId,
                Username = message.UserName
            };

            await _timelineGraphRepository.CreateUserAsync(user);
        }
    }
}
