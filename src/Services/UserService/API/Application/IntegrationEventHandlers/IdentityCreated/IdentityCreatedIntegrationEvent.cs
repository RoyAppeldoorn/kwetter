using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application.IntegrationEventHandlers.IdentityCreated
{
    public class IdentityCreatedIntegrationEvent
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
