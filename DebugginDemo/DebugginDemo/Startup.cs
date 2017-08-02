using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DebugginDemo.Startup))]
namespace DebugginDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
