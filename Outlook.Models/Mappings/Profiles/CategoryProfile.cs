using AutoMapper;
using Outlook.Models.Core.Dtos;
using Outlook.Models.Core.Models;

namespace Outlook.Models.Mappings.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategorySummaryDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
