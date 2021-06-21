using Kwetter.Services.Common.Domain;
using Kwetter.Services.FollowService.API.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Domain
{
    public class Follow : Entity
    {
        private bool _unfollowed;
        private Guid followerId;
        private Guid followingId;
        private User follower;
        private User following;

        public Guid FollowingId => followingId;

        public virtual User Following
        {
            get => following;
            private set
            {
                following = value;
                followingId = value.Id;
            }
        }

        public Guid FollowerId => followerId;

        public virtual User Follower
        {
            get => follower;
            private set
            {
                follower = value;
                followerId = value.Id;
            }
        }

        public DateTime FollowDateTime { get; private set; }

        /// <summary>
        /// EF constructor...
        /// </summary>
        protected Follow() { }

        public Follow(User follower, User following)
        {
            Follower = follower;
            Following = following;
            FollowDateTime = DateTime.UtcNow;
        }

        public bool Unfollow()
        {
            if (_unfollowed)
                throw new FollowDomainException("The following is already unfollowed.");
            _unfollowed = true;
            return _unfollowed;
        }
    }
}
