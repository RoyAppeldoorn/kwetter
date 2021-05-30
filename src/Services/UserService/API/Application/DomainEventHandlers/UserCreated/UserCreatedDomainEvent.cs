using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application.DomainEventHandlers.UserCreated
{
    public class UserCreatedDomainEvent
    {
        public Guid UserId { get; private set; }

        public string UserName { get; private set; }

        public UserCreatedDomainEvent(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
