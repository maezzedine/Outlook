using AutoMapper;
using backend.Models;
using backend.Models.Dtos;

namespace backend.Mappings.Profiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueSummaryDto>()
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume));

            CreateMap<Issue, IssueDto>()
                .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src.Articles));
        }
    }
}
