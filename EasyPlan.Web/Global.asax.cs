using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EasyPlan.Infrastructure;

namespace EasyPlan.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ContainerConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_ReleaseRequestState()
        {
            IUnitOfWork unitOfwork = DependencyResolver.Current.GetService<IUnitOfWork>();

            unitOfwork.Save();
        }
    }
}
