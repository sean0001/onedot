using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(one.OneDot.Startup))]
namespace one.OneDot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
