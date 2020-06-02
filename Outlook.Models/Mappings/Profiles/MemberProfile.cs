using AutoMapper;
using Outlook.Models.Core.Dtos;
using Outlook.Models.Core.Models;
using System.Linq;

namespace Outlook.Models.Mappings.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            // TODO: Link to helper functions class library
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
