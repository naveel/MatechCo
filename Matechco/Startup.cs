using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Matechco.Startup))]
namespace Matechco
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
