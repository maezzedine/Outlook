using AutoMapper;
using Outlook.Server.Models;
using Outlook.Server.Models.Dtos;
using Outlook.Server.Models.Interfaces;
using Outlook.Server.Services;
using System.Linq;

namespace Outlook.Server.Mappings.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberSummaryDto>()
                .ForMember(dest => dest.numberOfArticles, opt => opt.MapFrom(src => src.Articles.Count))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => MemberService.EnglishPositions.Contains(src.Position) ? Language.English.ToString() : Language.Arabic.ToString()))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.ToString().Replace('_', ' ')));

            CreateMap<Member, MemberDto>()
                .ForMember(dest => dest.numberOfArticles, opt => opt.MapFrom(src => src.Articles.Count))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => MemberService.EnglishPositions.Contains(src.Position) ? Language.English.ToString() : Language.Arabic.ToString()))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.ToString().Replace('_', ' ')));
        }
    }
}
