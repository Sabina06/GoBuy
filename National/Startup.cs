using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(National.Startup))]
namespace National
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
