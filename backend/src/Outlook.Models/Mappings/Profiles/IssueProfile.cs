using AutoMapper;
using Outlook.Models.Core.Dtos;
using Outlook.Models.Core.Models;

namespace Outlook.Models.Mappings.Profiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueDto>();
        }
    }
}
