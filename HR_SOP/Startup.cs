using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HR_SOP.Startup))]
namespace HR_SOP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
