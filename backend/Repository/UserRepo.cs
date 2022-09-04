using System.Diagnostics;
using backend.Model;
using backend.Services.Security;

namespace backend.Repository;

public class UserRepo
{
    private readonly Dictionary<string, ApplicationUser> _userByEmail = new();

    public void Add(ApplicationUser applicationUser)
    {
        applicationUser.Id = Guid.NewGuid();
        _userByEmail[applicationUser.Email] = applicationUser;
    }

    public ApplicationUser? FindByUsername(string username)
    {
        return _userByEmail.Values.FirstOrDefault(u => u.Username == username);
    }

    public ApplicationUser? FindByEmail(string email)
    {
        return _userByEmail.GetValueOrDefault(email);
    }

    public ApplicationUser? FromHttpContextOrNull(HttpContext httpContext)
    {
        var email = httpContext.User.GetEmail();
        Debug.Assert(email is not null);
        return FindByEmail(email);
    }

    public ApplicationUser FromHttpContext(HttpContext httpContext)
    {
        var userOrNull = FromHttpContextOrNull(httpContext);
        if (userOrNull is null)
            throw new InvalidOperationException("User does not exist");
        return userOrNull;
    }
}