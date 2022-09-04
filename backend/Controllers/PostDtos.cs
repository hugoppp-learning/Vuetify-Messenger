using backend.Model;

namespace backend.Controllers;

public record CreatePostDto(string Message);

public record PostDto(Guid Id, string Username, string? ProfilePicture, string Message, int Likes, bool Liked)
{
    public PostDto(Post post,
        Guid currentUserId) : this(
        post.Id,
        post.Username,
        post.ProfilePicture,
        post.Message,
        post.LikedUserIds.Count,
        post.LikedUserIds.Contains(currentUserId))
    {
    }

    public static PostDto New(Post post)
    {
        return new PostDto(post.Id, post.Username, post.ProfilePicture, post.Message, 0, false);
    }
};