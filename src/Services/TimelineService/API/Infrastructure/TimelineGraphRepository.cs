using Kwetter.Services.TimelineService.API.Domain;
using Neo4jClient;
using System;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Infrastructure
{
    public class TimelineGraphRepository : ITimelineGraphRepository
    {
        private readonly IGraphClient _client;

        public TimelineGraphRepository(IGraphClient client)
        {
            _client = client;
        }

        public async Task CreateFollowerAsync(Follow follow)
        {
            await _client.Cypher
                .Match("(follower:User)", "(following:User)")
                .Where((User follower) => follower.Id == follow.FollowerId)
                .AndWhere((User following) => following.Id == follow.FollowingId)
                .Create("(following)-[:FOLLOWED_BY]->(follower)")
                .ExecuteWithoutResultsAsync();
        }

        public async Task CreateKweetAsync(Kweet newKweet)
        {
            await _client.Cypher
                .Match("(user:User)")
                .Where((User user) => user.Id == newKweet.UserId)
                .Create("(kweet:Kweet {kweet})-[:KWEETED_BY]->(user)")
                .WithParam("kweet", newKweet)
                .ExecuteWithoutResultsAsync();
        }

        public async Task CreateKweetLikeAsync(KweetLike kweetLike)
        {
            await _client.Cypher
                .Match("(user:User)", "(kweet:Kweet)")
                .Where((User user) => user.Id == kweetLike.UserId)
                .AndWhere((Kweet kweet) => kweet.Id == kweetLike.KweetId)
                .Create("(kweet)-[:LIKED_BY]->(user)")
                .ExecuteWithoutResultsAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _client.Cypher
                .Merge("(user:User { Id: {id} })")
                .OnCreate()
                .Set("user = {user}")
                .WithParams(new
                {
                    id = user.Id,
                    user
                })
                .ExecuteWithoutResultsAsync();
        }

        public Task DeleteFollowerAsync(Guid followerId, Guid followingId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteKweetLikeAsync(Guid userId, Guid kweetId)
        {
            throw new NotImplementedException();
        }

        public Task<Timeline> GetPaginatedTimelineAsync(Guid userId, uint pageNumber, uint pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
