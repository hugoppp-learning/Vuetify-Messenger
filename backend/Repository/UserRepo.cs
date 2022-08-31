using backend.Services;

namespace backend.Repository;

public class UserRepo
{
    private readonly Dictionary<string, ApplicationUser> _userByEmail = new();

    public void Add(ApplicationUser applicationUser)
    {
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
}