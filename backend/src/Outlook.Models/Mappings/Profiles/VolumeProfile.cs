using AutoMapper;
using Outlook.Models.Core.Dtos;
using Outlook.Models.Core.Models;

namespace Outlook.Models.Mappings.Profiles
{
    public class VolumeProfile : Profile
    {
        public VolumeProfile()
        {
            CreateMap<Volume, VolumeSummaryDto>();
            CreateMap<Volume, VolumeDto>();
        }
    }
}
