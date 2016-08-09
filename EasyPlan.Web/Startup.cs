using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EasyPlan.Web.Startup))]
namespace EasyPlan.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}