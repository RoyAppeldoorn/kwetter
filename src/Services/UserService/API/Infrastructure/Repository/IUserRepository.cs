using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.UserService.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Infrastructure.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User Create(User user);

        User Update(User user);

        Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<User> FindByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}
