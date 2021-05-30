using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application
{
    public sealed class UserIntegrationException : Exception
    {
        public UserIntegrationException(string message) : base(message)
        {
        }
    }
}
