using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Dto.Kweet
{
    public class KweetDto
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public ICollection<KweetLikeDto> Likes { get; set; }
    }
}
