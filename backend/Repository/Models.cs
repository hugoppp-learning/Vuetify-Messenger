using AutoMapper;
using backend.Model;

namespace backend.Repository;

public static class Discriminator
{
    public const string User = "user";
    public const string Post = "post";
    public const string PostInteraction = "postInteraction";
}

public class DbMapperProfile : Profile
{
    public DbMapperProfile()
    {
        CreateMap<ApplicationUser, DbApplicationUser>();
        CreateMap<Post, DbPost>();
        CreateMap<PostInteraction, DbPostInteraction>();
    }
}

public class DbApplicationUser : ApplicationUser
{
    public string Discriminator => Repository.Discriminator.User;
}
public class DbPost : Post
{
    public string Discriminator => Repository.Discriminator.Post;
}

public class DbPostInteraction : PostInteraction
{
    public string Discriminator => Repository.Discriminator.PostInteraction;
}
