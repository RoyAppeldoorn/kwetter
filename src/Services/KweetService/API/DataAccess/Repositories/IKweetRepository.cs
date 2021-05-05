using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.KweetService.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.DataAccess.Repositories
{
    public interface IKweetRepository: IRepository<Kweet>
    {
        Kweet Create(Kweet kweet);

        Task<Kweet> FindAsync(Guid kweetId, CancellationToken cancellationToken);

        Task<IEnumerable<Kweet>> FindKweetsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
