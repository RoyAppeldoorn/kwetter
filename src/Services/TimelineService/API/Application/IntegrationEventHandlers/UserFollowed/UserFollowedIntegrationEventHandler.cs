using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.TimelineService.API.Domain;
using Kwetter.Services.TimelineService.API.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserFollowed
{
    public class UserFollowedIntegrationEventHandler : IMessageHandler<UserFollowedIntegrationEvent>
    {
        private readonly ITimelineGraphRepository _timelineGraphRepository;

        public UserFollowedIntegrationEventHandler(ITimelineGraphRepository timelineGraphRepository)
        {
            _timelineGraphRepository = timelineGraphRepository ?? throw new ArgumentNullException(nameof(timelineGraphRepository));
        }

        public async Task HandleMessageAsync(string messageType, UserFollowedIntegrationEvent message, CancellationToken cancellationToken)
        {
            Follow follow = new()
            {
                FollowerId = message.FollowerId,
                FollowingId = message.FollowingId,
            };

            await _timelineGraphRepository.CreateFollowerAsync(follow);
        }
    }
}
