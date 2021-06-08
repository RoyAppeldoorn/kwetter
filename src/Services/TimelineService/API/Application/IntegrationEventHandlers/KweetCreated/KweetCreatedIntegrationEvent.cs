using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetCreated
{
    public class KweetCreatedIntegrationEvent
    {
        public Guid KweetId { get; set; }

        public Guid UserId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
