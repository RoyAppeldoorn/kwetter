using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.TimelineService.API.Domain;
using Kwetter.Services.TimelineService.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetLiked
{
    public class KweetLikedIntegrationEventHandler : IMessageHandler<KweetLikedIntegrationEvent>
    {
        private readonly ITimelineGraphRepository _timelineGraphRepository;

        public KweetLikedIntegrationEventHandler(ITimelineGraphRepository timelineGraphRepository)
        {
            _timelineGraphRepository = timelineGraphRepository ?? throw new ArgumentNullException(nameof(timelineGraphRepository));
        }

        public async Task HandleMessageAsync(string messageType, KweetLikedIntegrationEvent message, CancellationToken cancellationToken)
        {
            KweetLike kweetLike = new()
            {
                KweetId = message.KweetId,
                UserId = message.UserId,
            };

            await _timelineGraphRepository.CreateKweetLikeAsync(kweetLike);
        }
    }
}
