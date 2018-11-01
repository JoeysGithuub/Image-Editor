using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageEdit.Startup))]
namespace ImageEdit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
