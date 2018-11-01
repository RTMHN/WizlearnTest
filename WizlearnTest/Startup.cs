using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WizlearnTest.Startup))]
namespace WizlearnTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
