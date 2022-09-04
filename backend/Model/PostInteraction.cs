namespace backend.Model;

public enum PostInteractionType
{
    Like
}

public class PostInteraction
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid InteractionUserId { get; init; }
    public Guid PostId { get; init; }
    public PostInteractionType InteractionType { get; init; }
}