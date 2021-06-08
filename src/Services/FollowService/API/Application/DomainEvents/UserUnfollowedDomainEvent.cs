using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.DomainEvents.UserUnfollowed
{
    public class UserUnfollowedDomainEvent
    {
        public Guid FollowingId { get; private set; }

        public Guid FollowerId { get; private set; }

        public UserUnfollowedDomainEvent(Guid followingId, Guid followerId)
        {
            FollowingId = followingId;
            FollowerId = followerId;
        }
    }
}
