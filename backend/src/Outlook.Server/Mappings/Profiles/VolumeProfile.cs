using AutoMapper;
using Outlook.Server.Models;
using Outlook.Server.Models.Dtos;

namespace Outlook.Server.Mappings.Profiles
{
    public class VolumeProfile : Profile
    {
        public VolumeProfile()
        {
            CreateMap<Volume, VolumeDto>();
        }
    }
}
