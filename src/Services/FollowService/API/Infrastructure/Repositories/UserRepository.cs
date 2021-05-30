using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.FollowService.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FollowDbContext _followContext;

        public IUnitOfWork UnitOfWork => _followContext;

        public UserRepository(FollowDbContext followDbContext)
        {
            _followContext = followDbContext ?? throw new ArgumentNullException(nameof(followDbContext));
        }

        public User Create(User user)
        {
            return _followContext.Users
                .Add(user).Entity;
        }

        public async ValueTask<User> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _followContext.Users
                .FindAsync(new object[] { id }, cancellationToken);

            //_followContext.Entry(user).
        }
    }
}
