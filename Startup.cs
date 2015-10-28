using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectBugTracker.Startup))]
namespace ProjectBugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
