using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetUnliked
{
    public class KweetUnlikedIntegrationEvent
    {
        public Guid KweetId { get; set; }

        public Guid UserId { get; set; }
    }
}
