using Kwetter.Services.TimelineService.API.Domain;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
                .Create("(kweet:Kweet $kweet)-[:KWEETED_BY]->(user)")
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
                .Merge("(user:User { Id: $id })")
                .OnCreate()
                .Set("user = $user")
                .WithParams(new
                {
                    id = user.Id,
                    user
                })
                .ExecuteWithoutResultsAsync();
        }

        public async Task DeleteFollowerAsync(Guid followerId, Guid followingId)
        {
            await _client.Cypher
                .Match("(following:User)-[follow:FOLLOWED_BY]->(follower:User)")
                .Where((User following) => following.Id == followingId)
                .AndWhere((User follower) => follower.Id == followerId)
                .Delete("follow")
                .ExecuteWithoutResultsAsync();
        }

        public async Task DeleteKweetLikeAsync(Guid userId, Guid kweetId)
        {
            await _client.Cypher
                .Match("(user:User)-[like:LIKED_BY]->(kweet:Kweet)")
                .Where((User user) => user.Id == userId)
                .AndWhere((Kweet kweet) => kweet.Id == kweetId)
                .Delete("like")
                .ExecuteWithoutResultsAsync();
        }

        public async Task<Timeline> GetPaginatedTimelineAsync(Guid userId, uint pageNumber, uint pageSize)
        {
            Timeline timeline = new()
            {
                UserId = userId,
                Kweets = new List<TimelineKweet>(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var query = _client.Cypher
                .Match("(user:User)<-[:FOLLOWED_BY]-(user2:User)<-[:KWEETED_BY]-(kweet:Kweet)")
                .Where((User user) => user.Id == userId)
                .Skip((int)pageNumber * (int)pageSize)
                .Limit((int)pageSize)
                .Return((user, kweet) => new
                {
                    User = user.As<User>(),
                    Kweets = kweet.CollectAs<Kweet>(),
                });
            var kek = (await query.ResultsAsync);
            //timeline.Kweets = (await query.ResultsAsync).ToList();
            
            return timeline;
        }
    }
}
