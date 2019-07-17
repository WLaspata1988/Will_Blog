using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Will_Blog.Startup))]
namespace Will_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
