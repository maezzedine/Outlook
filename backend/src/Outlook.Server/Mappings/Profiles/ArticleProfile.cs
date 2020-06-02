using AutoMapper;
using Outlook.Server.Models;
using Outlook.Server.Models.Dtos;

namespace Outlook.Server.Mappings.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()));
        }
    }
}
