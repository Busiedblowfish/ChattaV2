using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chatta.Startup))]
namespace Chatta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //Register the hub route
            app.MapSignalR();
        }
    }
}
