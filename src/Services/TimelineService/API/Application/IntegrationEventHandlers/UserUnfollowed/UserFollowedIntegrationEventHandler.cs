using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.TimelineService.API.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserUnfollowed
{
    public class UserUnfollowedIntegrationEventHandler : IMessageHandler<UserUnfollowedIntegrationEvent>
    {
        private readonly ITimelineGraphRepository _timelineGraphRepository;

        public UserUnfollowedIntegrationEventHandler(ITimelineGraphRepository timelineGraphRepository)
        {
            _timelineGraphRepository = timelineGraphRepository ?? throw new ArgumentNullException(nameof(timelineGraphRepository));
        }

        public async Task HandleMessageAsync(string messageType, UserUnfollowedIntegrationEvent message, CancellationToken cancellationToken)
        {
            await _timelineGraphRepository.DeleteFollowerAsync(message.FollowerId, message.FollowingId);
        }
    }
}
