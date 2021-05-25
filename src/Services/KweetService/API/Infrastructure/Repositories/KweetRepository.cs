using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.KweetService.API.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Infrastructure.Repositories
{
    public class KweetRepository : IKweetRepository
    {
        private readonly KweetDbContext _kweetContext;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _kweetContext;
            }
        }

        public KweetRepository(KweetDbContext kweetDbContext)
        {
            _kweetContext = kweetDbContext ?? throw new ArgumentNullException(nameof(kweetDbContext));
        }

        public Kweet Create(Kweet kweet)
        {
            return _kweetContext.Kweets.Add(kweet).Entity;
        }

        public async ValueTask<Kweet> FindAsync(Guid kweetId, CancellationToken cancellationToken)
        {
            return await _kweetContext.Kweets.FindAsync(new object[] { kweetId }, cancellationToken);
        }

        public async Task<IEnumerable<Kweet>> FindKweetsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _kweetContext.Kweets
                .Where(kweet => kweet.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
