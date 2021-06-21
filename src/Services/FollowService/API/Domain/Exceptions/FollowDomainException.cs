using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Domain.Exceptions
{
    internal sealed class FollowDomainException : Exception
    {
        internal FollowDomainException(string message) : base(message)
        {
        }
    }
}
