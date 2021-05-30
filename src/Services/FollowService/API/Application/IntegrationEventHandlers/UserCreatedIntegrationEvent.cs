using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.IntegrationEventHandlers
{
    public class UserCreatedIntegrationEvent
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
