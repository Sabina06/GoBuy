using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForFrontPage.Startup))]
namespace ForFrontPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
