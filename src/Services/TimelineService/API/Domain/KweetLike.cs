using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Domain
{
    public class KweetLike
    {
        public Guid KweetId { get; set; }

        public Guid UserId { get; set; }

        public DateTime LikedDateTime { get; set; }
    }
}
