using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.TimelineService.API.Domain;
using Kwetter.Services.TimelineService.API.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetUnliked
{
    public class KweetUnlikedIntegrationEventHandler : IMessageHandler<KweetUnlikedIntegrationEvent>
    {
        private readonly ITimelineGraphRepository _timelineGraphRepository;

        public KweetUnlikedIntegrationEventHandler(ITimelineGraphRepository timelineGraphRepository)
        {
            _timelineGraphRepository = timelineGraphRepository ?? throw new ArgumentNullException(nameof(timelineGraphRepository));
        }

        public async Task HandleMessageAsync(string messageType, KweetUnlikedIntegrationEvent message, CancellationToken cancellationToken)
        {
            await _timelineGraphRepository.DeleteKweetLikeAsync(message.UserId, message.KweetId);
        }
    }
}
