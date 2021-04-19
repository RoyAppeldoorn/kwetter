using Kwetter.Services.KweetService.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.DataAccess.Repositories
{
    public interface IKweetRepository
    {
        Task<bool> CreateKweet(Kweet kweet);

        Task<Kweet> FindAsync(Guid kweetId, CancellationToken cancellationToken);

        Task<IEnumerable<Kweet>> FindKweetsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
