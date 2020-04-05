using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebForm6.Startup))]
namespace WebForm6
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
