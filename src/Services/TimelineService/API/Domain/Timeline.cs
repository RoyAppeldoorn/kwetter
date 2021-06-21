using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Domain
{
    public class Timeline
    {
        public Guid UserId { get; set; }

        public List<TimelineKweet> Kweets { get; set; }

        public uint PageNumber { get; set; }

        public uint PageSize { get; set; }
    }
}
