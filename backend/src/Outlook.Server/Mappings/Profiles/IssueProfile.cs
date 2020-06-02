using AutoMapper;
using Outlook.Server.Models;
using Outlook.Server.Models.Dtos;

namespace Outlook.Server.Mappings.Profiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueDto>();
        }
    }
}
