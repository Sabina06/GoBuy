using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JStest.Startup))]
namespace JStest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
