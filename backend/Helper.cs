namespace backend;

public interface IHasId
{
    int Id { get; }
}
public static class Helper
{
    //temp
    public static int NextId(this IEnumerable<IHasId> o)
    {
        if (!o.Any())
            return 1;
        return o.Max(o => o.Id) + 1;
    }
    
}