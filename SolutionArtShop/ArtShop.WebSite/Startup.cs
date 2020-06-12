using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtShop.WebSite.Startup))]
namespace ArtShop.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
