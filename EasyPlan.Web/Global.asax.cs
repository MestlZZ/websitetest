using System;
using System.Web.Optimization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.ModelBinding;

namespace EasyPlan.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ModelBinderProviders.BinderProviders.Add(new EntityModelBinderProvider());
            ContainerConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_ReleaseRequestState()
        {
            IUnitOfWork unitOfwork = DependencyResolver.Current.GetService<IUnitOfWork>();

            unitOfwork.Save();
        }

        protected void Application_Error()
        {
            Exception exc = Server.GetLastError();

            if (exc is ArgumentValidationException)
            {
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;

                var exception = (exc as ArgumentValidationException);

                Response.StatusCode = exception.StatusCode;
                Response.StatusDescription = exception.Message;
            }
        }
    }
}
