using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentTracking.Web.Startup))]
namespace StudentTracking.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
