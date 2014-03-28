using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FailTracker.Web.Startup))]
namespace FailTracker.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
