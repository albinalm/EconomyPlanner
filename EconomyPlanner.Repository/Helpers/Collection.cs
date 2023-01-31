namespace EconomyPlanner.Repository.Helpers;

public static class Collection
{
    public static ICollection<T> Empty<T>() => Enumerable.Empty<T>().ToList();
}