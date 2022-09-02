namespace backend.Model;

public class Post : IHasId
{
    public int Id { get; init; }
    public string Username { get; init; }
    public string? ProfilePicture { get; init; }
    public string Message { get; init; }
    public HashSet<int> LikedUserIds { get; } = new();
}