using Kwetter.Services.Common.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Models
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
