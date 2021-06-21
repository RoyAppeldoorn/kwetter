using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.DomainEvents.KweetLiked
{
    public class KweetLikedDomainEvent
    {
        public Guid KweetId { get; private set; }

        public Guid KweetUserId { get; private set; }

        public Guid UserId { get; private set; }

        public KweetLikedDomainEvent(Guid kweetId, Guid kweetUserId, Guid userId)
        {
            KweetId = kweetId;
            KweetUserId = kweetUserId;
            UserId = userId;
        }
    }
}
