using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsCollector.Startup))]
namespace NewsCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
