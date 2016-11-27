using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InterShop.Startup))]
namespace InterShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
