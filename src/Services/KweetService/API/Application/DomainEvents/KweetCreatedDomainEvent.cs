using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.DomainEvents.KweetCreated
{
    public class KweetCreatedDomainEvent
    {
        public Guid KweetId { get; private set; }

        public Guid UserId { get; private set; }

        public string Message { get; private set; }

        public DateTime CreatedDateTime { get; private set; }

        public KweetCreatedDomainEvent(Guid kweetId, Guid userId, string message, DateTime createdDateTime)
        {
            KweetId = kweetId;
            UserId = userId;
            Message = message;
            CreatedDateTime = createdDateTime;
        }
    }
}
