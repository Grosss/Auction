using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcPL.Startup))]
namespace MvcPL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //
        }
    }
}
