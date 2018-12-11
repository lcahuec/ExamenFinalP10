using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalP10.Startup))]
namespace FinalP10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
