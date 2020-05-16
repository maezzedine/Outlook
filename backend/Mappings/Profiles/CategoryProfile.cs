using AutoMapper;
using backend.Models;
using backend.Models.Dtos;

namespace backend.Mappings.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategorySummaryDto>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.TagName))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src.Articles))
                .ForMember(dest => dest.JuniorEditors, opt => opt.MapFrom(src => src.JuniorEditors));
        }
    }
}
