using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleAjaxApp.Startup))]
namespace SimpleAjaxApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
