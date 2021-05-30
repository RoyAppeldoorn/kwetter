using Kwetter.Services.Common.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Kwetter.Services.KweetService.API.Domain
{
    public class Kweet : Entity
    {
        private HashSet<KweetLike> likes;

        public Guid UserId { get; private set; }

        public string Message { get; private set; }

        public DateTime CreatedDateTime { get; private set; }

        public IReadOnlySet<KweetLike> Likes => likes;

        /// <summary>
        /// EF Constructor 
        /// </summary>
        protected Kweet() 
        {
            likes = new HashSet<KweetLike>(new KweetLikeEqualityComparer());
        } 

        public Kweet(Guid id, Guid userId, string message) : this()
        {
            Id = id;
            UserId = userId;
            Message = message;
            CreatedDateTime = DateTime.UtcNow;
        }

        public bool AddLike(Guid userId)
        {
            KweetLike like = new(Id, userId);
            if (likes.Contains(like))
                return false;
            likes.Add(like);
            return true;
        }

        public bool RemoveLike(Guid userId)
        {
            KweetLike kweetLike = likes.FirstOrDefault(kweet => kweet.UserId == userId);
            if (kweetLike == null)
                return false;
            likes.Remove(kweetLike);
            return true;
        }

        private sealed class KweetLikeEqualityComparer : IEqualityComparer<KweetLike>
        {
            /// <summary>
            /// The like's uniqueness is determined by the user id.
            /// </summary>
            /// <param name="x">Like x to compare.</param>
            /// <param name="y">Like y to compare.</param>
            /// <returns>Returns a boolean to indicate whether the likes are equal.</returns>
            public bool Equals(KweetLike x, KweetLike y) => x.UserId == y.UserId;

            public int GetHashCode([DisallowNull] KweetLike obj) => obj.UserId.GetHashCode();
        }
    }
}
