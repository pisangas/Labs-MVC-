using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Autenticacion.Startup))]
namespace MVC_Autenticacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
