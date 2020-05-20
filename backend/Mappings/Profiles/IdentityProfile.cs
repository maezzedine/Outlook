using AutoMapper;
using backend.Areas.Identity;
using backend.Models.Dtos;

namespace backend.Mappings.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<OutlookUser, UserDto>();
        }
    }
}
