using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrossoverTask.Web.Startup))]
namespace CrossoverTask.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
