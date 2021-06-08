using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.DomainEvents
{
    public class KweetUnlikedDomainEvent
    {
        public Guid KweetId { get; private set; }

        public Guid UserId { get; private set; }

        public KweetUnlikedDomainEvent(Guid kweetId, Guid userId)
        {
            KweetId = kweetId;
            UserId = userId;
        }
    }
}
