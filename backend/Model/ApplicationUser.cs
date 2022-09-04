namespace backend.Model;
public class ApplicationUser 
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Role> Roles { get; set; }
    public string Username { get; set; }
    public string ProfilePicture { get; set; }
}

public enum Role
{
    Verified,
}