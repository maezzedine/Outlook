using AutoMapper;
using backend.Models;
using backend.Models.Dtos;

namespace backend.Mappings.Profiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueDto>();
        }
    }
}
