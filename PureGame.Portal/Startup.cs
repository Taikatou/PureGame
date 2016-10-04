using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PureGame.Portal.Startup))]
namespace PureGame.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
