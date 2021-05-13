using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.KweetService.API.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Infrastructure.Repositories
{
    public interface IKweetRepository: IRepository<Kweet>
    {
        Kweet Create(Kweet kweet);

        ValueTask<Kweet> FindAsync(Guid kweetId, CancellationToken cancellationToken);

        Task<IEnumerable<Kweet>> FindKweetsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
