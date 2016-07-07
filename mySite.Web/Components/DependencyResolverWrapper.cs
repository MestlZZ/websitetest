using System.Web.Mvc;
using mySite.Infrastructure;

namespace mySite.Web.Components
{
    public class DependencyResolverWrapper : IDependencyResolverWrapper
    {
        public T GetService<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}