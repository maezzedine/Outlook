using AutoMapper;
using Outlook.Models.Core.Dtos;
using Outlook.Models.Core.Models;

namespace Outlook.Models.Mappings.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<OutlookUser, UserDto>();
        }
    }
}
