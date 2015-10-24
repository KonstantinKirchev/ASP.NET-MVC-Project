using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoContests.App.Startup))]
namespace PhotoContests.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
