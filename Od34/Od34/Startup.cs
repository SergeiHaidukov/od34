using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Od34.Startup))]
namespace Od34
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
