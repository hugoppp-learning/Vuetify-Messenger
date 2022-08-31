namespace backend.Model;

public class Post
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string ProfilePicture { get; init; }
    public string Message { get; init; }
    public int Likes { get; init; }
    public bool Liked { get; init; }
}