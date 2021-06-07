using AutoMapper;
using Kwetter.Services.TimelineService.API.Application.Queries;
using Kwetter.Services.TimelineService.API.Domain;

namespace Kwetter.Services.TimelineService.API.Application.Mapping
{
    public class TimelineKweetProfile : Profile
    {
        public TimelineKweetProfile()
        {
            CreateMap<KweetDto, TimelineKweet>().ReverseMap();
        }
    }
}
