using Kwetter.Services.Common.Domain;
using Kwetter.Services.FollowService.API.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Domain
{
    public class User : Entity
    {
        private List<Follow> followers;
        private List<Follow> followings;

        public string Username { get; private set; }

        
        public virtual IReadOnlyCollection<Follow> Followers => followers.AsReadOnly();

        public virtual IReadOnlyCollection<Follow> Followings => followings.AsReadOnly();

        /// <summary>
        /// EF constructor...
        /// </summary>
        protected User() 
        {
            followers = new List<Follow>();
            followings = new List<Follow>();
        }

        public User(Guid userId, string username) : this()
        {
            Id = userId;
            Username = username;
        }

        public bool Follow(User otherUser)
        {
            Follow follow = followings.Find(follow => follow.FollowerId == Id && follow.FollowingId == otherUser.Id);
            if (follow != default)
                return false;
            follow = new(this, otherUser);
            followings.Add(follow);
            otherUser.AddFollower(follow);
            return true;
        }

        public bool Unfollow(User otherUser)
        {
            Follow follow = followings.Find(follow => follow.FollowerId == Id && follow.FollowingId == otherUser.Id);
            if (follow == default)
                return false;
            if (follow.Unfollow())
            {
                followings.Remove(follow);
                otherUser.RemoveFollower(follow);
                return true;
            }
            return false;
        }

        private bool RemoveFollower(Follow follow)
        {
            return followers.Remove(follow);
        }

        private void AddFollower(Follow follow)
        {
            followers.Add(follow);
        }
    }
}
