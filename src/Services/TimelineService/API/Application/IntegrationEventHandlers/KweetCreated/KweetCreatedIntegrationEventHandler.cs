using Kwetter.Services.Common.Infrastructure.Messaging;
using Kwetter.Services.TimelineService.API.Domain;
using Kwetter.Services.TimelineService.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetCreated
{
    public class KweetCreatedIntegrationEventHandler : IMessageHandler<KweetCreatedIntegrationEvent>
    {
        private readonly ITimelineGraphRepository _timelineGraphRepository;

        public KweetCreatedIntegrationEventHandler(ITimelineGraphRepository timelineGraphRepository)
        {
            _timelineGraphRepository = timelineGraphRepository ?? throw new ArgumentNullException(nameof(timelineGraphRepository));
        }

        public async Task HandleMessageAsync(string messageType, KweetCreatedIntegrationEvent message, CancellationToken cancellationToken)
        {
            Kweet kweet = new()
            {
                Id = message.KweetId,
                UserId = message.UserId,
                Message = message.Message,
                CreatedDateTime = message.CreatedDateTime
            };

            await _timelineGraphRepository.CreateKweetAsync(kweet);
        }
    }
}
