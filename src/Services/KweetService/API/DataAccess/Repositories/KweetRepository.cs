using Kwetter.Services.KweetService.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.DataAccess.Repositories
{
    public class KweetRepository : IKweetRepository
    {
        private readonly KweetDbContext _kweetContext;

        public KweetRepository(KweetDbContext kweetDbContext)
        {
            _kweetContext = kweetDbContext ?? throw new ArgumentNullException(nameof(kweetDbContext));
        }

        public async Task<bool> CreateKweet(Kweet kweet)
        {
            _kweetContext.Kweets.Add(kweet);
            await _kweetContext.SaveChangesAsync();

            return true;
        }

        public async Task<Kweet> FindAsync(Guid kweetId, CancellationToken cancellationToken)
        {
            return await _kweetContext.Kweets.FindAsync(kweetId, cancellationToken);
        }

        public async Task<IEnumerable<Kweet>> FindKweetsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _kweetContext.Kweets
                .Where(kweet => kweet.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
