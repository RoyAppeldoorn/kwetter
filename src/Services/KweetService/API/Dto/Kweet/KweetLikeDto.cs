using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Dto.Kweet
{
    public class KweetLikeDto
    {
        public Guid KweetId { get; set; }

        public Guid UserId { get; set; }

        public DateTime LikedDateTime { get; private set; }
    }
}
