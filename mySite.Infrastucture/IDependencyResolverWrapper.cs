namespace mySite.Infrastructure
{
    public interface IDependencyResolverWrapper
    {
        T GetService<T>();
    }
}
