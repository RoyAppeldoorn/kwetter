using System;
using System.Collections.Generic;

namespace Kwetter.Services.KweetService.API.Application.Queries
{
    public class KweetDto
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public List<KweetLikeDto> Likes { get; set; }
    }
}
