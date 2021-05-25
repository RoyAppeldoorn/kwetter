using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Kwetter.Services.AuthorizationService.API.Application.Dtos;
using Kwetter.Services.AuthorizationService.API.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Infrastructure
{
    public class FirebaseTokenVerifier : ITokenVerifier
    {
        protected readonly FirebaseAuth _firebaseAuth;

        public FirebaseTokenVerifier(FirebaseApp firebaseApp)
        {
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp ?? throw new ArgumentNullException(nameof(firebaseApp)));
        }

        public async Task<TokenDto> VerifyIdTokenAsync(string idToken, CancellationToken cancellationToken = default)
        {
            FirebaseToken decodedToken = await _firebaseAuth.VerifyIdTokenAsync(idToken, cancellationToken);
            return new TokenDto
            {
                Subject = decodedToken.Subject,
                Audience = decodedToken.Audience,
                Issuer = decodedToken.Issuer,
                ExpirationTimeSeconds = decodedToken.ExpirationTimeSeconds,
                IssuedAtTimeSeconds = decodedToken.IssuedAtTimeSeconds,
                Claims = new Dictionary<string, ClaimDto>(decodedToken.Claims
                            .Select(kvp => new KeyValuePair<string, ClaimDto>(kvp.Key, new ClaimDto() { Name = kvp.Key, Value = kvp.Value as string })))
            };
        }
    }
}
