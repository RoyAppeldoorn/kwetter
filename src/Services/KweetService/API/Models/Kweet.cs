using Kwetter.Services.Common.API;
using System;
using System.Collections.Generic;

namespace Kwetter.Services.KweetService.API.Models
{
    public class Kweet : Entity
    {
        public Guid UserId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDateTime { get; private set; }

        public ICollection<KweetLike> Likes { get; set; }

        public Kweet()
        {
            CreatedDateTime = DateTime.UtcNow;
        }
    }
}
