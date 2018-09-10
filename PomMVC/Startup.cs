using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PomMVC.Startup))]
namespace PomMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
