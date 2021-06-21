using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetLiked
{
    public class KweetLikedIntegrationEvent
    {
        public Guid KweetId { get; set; }

        public Guid UserId { get; set; }
    }
}
