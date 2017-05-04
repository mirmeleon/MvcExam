using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SugarFactory.Web.Startup))]
namespace SugarFactory.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
           
        }
    }
}
