namespace backend.Services;

public class ApplicationUser
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Role> Roles { get; set; }
    public string Username { get; set; }
}

public enum Role
{
    Verified,
}