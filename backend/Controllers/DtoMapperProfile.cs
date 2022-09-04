using AutoMapper;
using backend.Model;

namespace backend.Controllers;

public class DtoMapperProfile : Profile
{
    public DtoMapperProfile()
    {
        CreateMap<Post, PostDto>()
            .ForCtorParam(
                nameof(PostDto.Likes),
                m => m.MapFrom(post => post.LikesCount)
            );
        
    }
}