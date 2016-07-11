namespace EasyPlan.Infrastructure
{
    public interface IDependencyResolverWrapper
    {
        T GetService<T>();
    }
}
