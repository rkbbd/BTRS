using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Form.Startup))]
namespace Form
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
