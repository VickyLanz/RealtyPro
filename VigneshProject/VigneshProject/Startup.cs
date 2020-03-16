using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VigneshProject.Startup))]
namespace VigneshProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
