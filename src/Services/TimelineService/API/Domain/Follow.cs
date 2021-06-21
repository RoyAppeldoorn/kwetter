using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Domain
{
    public class Follow
    {
        public Guid FollowingId { get; set; }

        public Guid FollowerId { get; set; }

        public DateTime FollowDateTime { get; set; }
    }
}
