using Kwetter.Services.AuthorizationService.API.Domain;
using Kwetter.Services.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityDbContext _identityContext;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _identityContext;
            }
        }

        public IdentityRepository(IdentityDbContext identityContext)
        {
            _identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        }

        public Identity Create(Identity identity)
        {
            return _identityContext.Identities
                .Add(identity).Entity;
        }

        public async Task<Identity> FindIdentityByOpenIdAsync(string openId, CancellationToken cancellationToken)
        {
            return await _identityContext.Identities
                .SingleOrDefaultAsync(identity => identity.OpenId == openId, cancellationToken);
        }

        public async Task<Identity> FindByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _identityContext.Identities
                .SingleOrDefaultAsync(identity => identity.UserName == userName.ToLower(), cancellationToken);
        }
    }
}
