using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningPlattform.Startup))]
namespace LearningPlattform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
