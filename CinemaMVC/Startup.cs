using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CinemaMVC.Startup))]
namespace CinemaMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
