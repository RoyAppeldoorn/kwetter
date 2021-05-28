using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Application.DomainEvents
{
    public class IdentityCreatedDomainEvent
    {
        public Guid UserId { get; private set; }

        public string UserName { get; private set; }

        public IdentityCreatedDomainEvent(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
