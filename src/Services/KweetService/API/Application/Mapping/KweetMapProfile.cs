using AutoMapper;
using Kwetter.Services.KweetService.API.Application.Queries;
using Kwetter.Services.KweetService.API.Domain;

namespace Kwetter.Services.KweetService.API.Application.Mapping
{
    public class KweetMapProfile : Profile
    {
        public KweetMapProfile()
        {
            CreateMap<Kweet, KweetDto>().ReverseMap();
            CreateMap<KweetLike, KweetLikeDto>().ReverseMap();
        }
    }
}
