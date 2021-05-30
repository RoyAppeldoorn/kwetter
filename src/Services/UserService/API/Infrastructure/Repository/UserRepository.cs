using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.UserService.API.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userContext;

        public IUnitOfWork UnitOfWork => _userContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userContext = userDbContext ?? throw new ArgumentNullException(nameof(userDbContext));
        }

        public User Create(User user)
        {
            return _userContext.Users
                .Add(user).Entity;
        }

        public async Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _userContext.Users
                .FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<User> FindByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            string loweredUserName = userName.ToLower();
            return await _userContext.Users
                .AsQueryable().Where(user => user.Username == loweredUserName)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public User Update(User user)
        {
            return _userContext.Update(user).Entity;
        }
    }
}
