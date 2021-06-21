using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserUnfollowed
{
    public class UserUnfollowedIntegrationEvent
    {
        public Guid FollowingId { get; set; }

        public Guid FollowerId { get; set; }
    }
}
