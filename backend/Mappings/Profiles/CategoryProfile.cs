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
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag.ToString()))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag.ToString()))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()));
        }
    }
}
