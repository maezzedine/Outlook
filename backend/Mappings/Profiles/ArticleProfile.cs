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
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Issue, opt => opt.MapFrom(src => src.Issue))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()))
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src.Member));
        }
    }
}
