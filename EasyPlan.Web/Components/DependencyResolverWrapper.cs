using System.Web.Mvc;
using EasyPlan.Infrastructure;

namespace EasyPlan.Web.Components
{
    public class DependencyResolverWrapper : IDependencyResolverWrapper
    {
        public T GetService<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}