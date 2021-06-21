using Kwetter.Services.TimelineService.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Infrastructure
{
    public interface ITimelineGraphRepository
    {
        public Task CreateFollowerAsync(Follow follow);

        public Task DeleteFollowerAsync(Guid followerId, Guid followingId);

        public Task CreateKweetAsync(Kweet kweet);

        public Task CreateKweetLikeAsync(KweetLike kweetLike);

        public Task DeleteKweetLikeAsync(Guid userId, Guid kweetId);

        public Task CreateUserAsync(User user);

        public Task<Timeline> GetPaginatedTimelineAsync(Guid userId, uint pageNumber, uint pageSize);
    }
}
