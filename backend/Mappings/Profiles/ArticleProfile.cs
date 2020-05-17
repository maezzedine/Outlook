using AutoMapper;
using backend.Models;
using backend.Models.Dtos;

namespace backend.Mappings.Profiles
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
