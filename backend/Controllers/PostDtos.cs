using backend.Model;

namespace backend.Controllers;

public record CreatePostDto(string Message);

public record PostDto(Guid Id, string Username, string? ProfilePicture, string Message, int Likes, bool Liked)
{
};