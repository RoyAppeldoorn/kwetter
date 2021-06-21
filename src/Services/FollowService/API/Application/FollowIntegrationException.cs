using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application
{
    public sealed class FollowIntegrationException : Exception
    {
        public FollowIntegrationException(string message) : base(message)
        {
        }
    }
}
