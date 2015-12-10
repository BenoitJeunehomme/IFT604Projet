using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IFT604Projet.Startup))]
namespace IFT604Projet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
