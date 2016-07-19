using System.Web.Mvc;
using System.Web.Routing;

namespace EasyPlan.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
               name: "Board",
               url: "board/{action}",
               defaults: new { controller = "Board" },
               namespaces: new[] { "EasyPlan.Controllers" }
            );

            routes.MapRoute(
               name: "Criterion",
               url: "board/criterion/{action}",
               defaults: new { controller = "Criterion" },
               namespaces: new[] { "EasyPlan.Controllers" }
            );

            routes.MapRoute(
               name: "Item",
               url: "board/item/{action}",
               defaults: new { controller = "Item" },
               namespaces: new[] { "EasyPlan.Controllers" }
            );

            routes.MapRoute(
                name: "Mark",
                url: "board/mark/{action}",
                defaults: new { controller = "Mark" },
                namespaces: new[] { "EasyPlan.Controllers" }
             );

            routes.MapRoute(
               name: "Default",
               url: "",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "EasyPlan.Controllers" }
            );
        }
    }
}
