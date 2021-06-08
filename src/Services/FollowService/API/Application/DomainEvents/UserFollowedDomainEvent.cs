using System;

namespace Kwetter.Services.FollowService.API.Application.DomainEvents.UserFollowed
{
    public class UserFollowedDomainEvent
    {
        public Guid FollowingId { get; private set; }

        public Guid FollowerId { get; private set; }

        public UserFollowedDomainEvent(Guid followingId, Guid followerId)
        {
            FollowingId = followingId;
            FollowerId = followerId;
        }
    }
}
