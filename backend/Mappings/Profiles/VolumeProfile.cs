using AutoMapper;
using backend.Models;
using backend.Models.Dtos;

namespace backend.Mappings.Profiles
{
    public class VolumeProfile : Profile
    {
        public VolumeProfile()
        {
            CreateMap<Volume, VolumeDto>()
                .ForMember(dest => dest.Issues, opt => opt.MapFrom(src => src.Issues));
        }
    }
}
