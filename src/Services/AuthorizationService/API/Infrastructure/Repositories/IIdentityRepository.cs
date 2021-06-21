using Kwetter.Services.AuthorizationService.API.Domain;
using Kwetter.Services.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Infrastructure.Repositories
{
    public interface IIdentityRepository : IRepository<Identity>
    {
        Identity Create(Identity identity);

        Task<Identity> FindIdentityByOpenIdAsync(string openId, CancellationToken cancellationToken);

        Task<Identity> FindByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}
