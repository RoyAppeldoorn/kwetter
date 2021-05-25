using Kwetter.Services.Common.Domain;
using System;

namespace Kwetter.Services.KweetService.API.Domain
{
    public class KweetLike : Entity
    {
        public Guid KweetId { get; private set; }

        public Guid UserId { get; private set; }

        public DateTime LikedDateTime { get; private set; }

        /// <summary>
        /// EF constructor
        /// </summary>
        protected KweetLike() { }

        public KweetLike(Guid kweetId, Guid userId)
        {
            KweetId = kweetId;
            UserId = userId;
            LikedDateTime = DateTime.UtcNow;
        }
    }
}
