using AutoMapper;
using Outlook.Server.Areas.Identity;
using Outlook.Server.Models.Dtos;

namespace Outlook.Server.Mappings.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<OutlookUser, UserDto>();
        }
    }
}
