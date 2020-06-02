using AutoMapper;
using backend.Models;
using backend.Models.Dtos;

namespace backend.Mappings.Profiles
{
    public class VolumeProfile : Profile
    {
        public VolumeProfile()
        {
            CreateMap<Volume, VolumeDto>();
        }
    }
}
