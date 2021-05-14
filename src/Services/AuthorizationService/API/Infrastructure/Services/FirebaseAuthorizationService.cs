using FirebaseAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Infrastructure.Services
{
    public class FirebaseAuthorizationService : FirebaseTokenVerifier, IAuthorizationService
    {
        public FirebaseAuthorizationService(FirebaseApp firebaseApp) : base(firebaseApp)
        {

        }

        public Task SetUserClaimsAsync(string openId, Dictionary<string, object> claims, CancellationToken cancellationToken)
        {
            return _firebaseAuth.SetCustomUserClaimsAsync(openId, claims, cancellationToken);
        }
    }
}
