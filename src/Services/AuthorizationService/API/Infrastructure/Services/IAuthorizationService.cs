using Kwetter.Services.AuthorizationService.API.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Infrastructure.Services
{
    public interface IAuthorizationService : ITokenVerifier
    {
        public Task SetUserClaimsAsync(string openId, Dictionary<string, object> claims, CancellationToken cancellationToken);
    }
}
