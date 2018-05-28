using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GladiatorProject.Startup))]
namespace GladiatorProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
