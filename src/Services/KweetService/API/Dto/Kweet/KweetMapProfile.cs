using AutoMapper;

namespace Kwetter.Services.KweetService.API.Dto.Kweet
{
    public class KweetMapProfile : Profile
    {
        public KweetMapProfile()
        {
            CreateMap<Models.Kweet, KweetDto>();
        }
    }
}
