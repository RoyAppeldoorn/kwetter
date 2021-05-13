using System;

namespace Kwetter.Services.KweetService.API.Application.Queries
{
    public class KweetLikeDto
    {
        public Guid KweetId { get; set; }

        public Guid UserId { get; set; }

        public DateTime LikedDateTime { get; set; }
    }
}
