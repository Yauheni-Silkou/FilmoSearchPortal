namespace FilmoSearchPortal.DAL.Repositories;

public static class RepositoryExtensions
{
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            collection?.Add(value);
        }
    }
}