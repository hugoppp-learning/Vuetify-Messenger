using backend.Model;

namespace backend.Controllers;

public record UserDto(string Email, List<Role> Roles, string Username, string ProfilePicture)
{
    public UserDto(ApplicationUser au) : this(au.Email, au.Roles, au.Username, au.ProfilePicture)
    {
    }
};