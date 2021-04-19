using Kwetter.Services.Common.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Models
{
    public class KweetLike : Entity
    {
        public Kweet Kweet { get; set; }

        public Guid UserId { get; set; }

        public DateTime LikedDateTime { get; private set; }

        public KweetLike()
        {
            LikedDateTime = DateTime.UtcNow;
        }
    }
}
