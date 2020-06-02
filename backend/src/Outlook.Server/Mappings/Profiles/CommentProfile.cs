using AutoMapper;
using Outlook.Server.Models;
using Outlook.Server.Models.Dtos;

namespace Outlook.Server.Mappings.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>();
        }
    }
}
