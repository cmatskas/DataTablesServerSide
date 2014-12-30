using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataTablesServerSide.Startup))]
namespace DataTablesServerSide
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
