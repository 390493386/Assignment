using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment.Web.Startup))]
namespace Assignment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //var builder = new ContainerBuilder();
            //var container = builder.Build();
            //app.UseAutofacMiddleware
        }
    }
}
