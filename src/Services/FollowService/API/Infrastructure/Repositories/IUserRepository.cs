using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.FollowService.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Create(User user);

        ValueTask<User> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
