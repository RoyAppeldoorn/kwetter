using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application
{
    public sealed class UserIntegrationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KweetIntegrationException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public UserIntegrationException(string message) : base(message)
        {
        }
    }
}
