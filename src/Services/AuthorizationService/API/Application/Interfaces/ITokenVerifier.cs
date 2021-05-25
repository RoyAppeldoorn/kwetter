using Kwetter.Services.AuthorizationService.API.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Application.Interfaces
{
    public interface ITokenVerifier
    {
        public Task<TokenDto> VerifyIdTokenAsync(string idToken, CancellationToken cancellationToken = default);
    }
}
