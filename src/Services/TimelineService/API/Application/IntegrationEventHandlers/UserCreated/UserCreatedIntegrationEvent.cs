using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserCreated
{
    public class UserCreatedIntegrationEvent
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
