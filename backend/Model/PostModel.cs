namespace backend.Model;

public class Post
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Username { get; init; }
    public string? ProfilePicture { get; init; }
    public string Message { get; init; }
    public int LikesCount { get; init; }
    public bool Liked { get; set; }
}