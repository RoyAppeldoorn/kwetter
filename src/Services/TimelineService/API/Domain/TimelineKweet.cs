using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Domain
{
    public class TimelineKweet
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Message { get; set; }

        public string Username { get; set; }

        public bool Liked { get; set; }

        public int LikeCount { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
