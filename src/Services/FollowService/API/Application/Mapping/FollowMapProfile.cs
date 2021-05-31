using AutoMapper;
using Kwetter.Services.FollowService.API.Application.Queries;
using Kwetter.Services.FollowService.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Mapping
{
    public class FollowMapProfile : Profile
    {
        //public FollowMapProfile()
        //{
        //    CreateMap<User, UserDto>(MemberList.Source);

        //    CreateMap<Follow, FollowDto>(MemberList.Source)
        //        .ForMember(x => x.Follower, m => m.MapFrom(s => s.Follower.Id))
        //        .ForMember(x => x.Follower, m => m.MapFrom(s => s.Follower.Id));
        //}
    }
}
