using AutoMapper;
using backend.Model;

namespace backend.Repository.Database;

public static class Discriminator
{
    public const string User = "user";
    public const string Post = "post";
}

public class DbMapperProfile : Profile
{
    public DbMapperProfile()
    {
        CreateMap<ApplicationUser, DbApplicationUser>();
        CreateMap<Post, DbPost>();
    }
}

public class DbApplicationUser : ApplicationUser
{
    public string Discriminator => Database.Discriminator.User;
}
public class DbPost : Post
{
    public string Discriminator => Database.Discriminator.Post;
}
