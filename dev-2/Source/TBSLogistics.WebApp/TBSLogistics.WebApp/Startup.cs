using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TBSLogistics.WebApp.Startup))]
namespace TBSLogistics.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
