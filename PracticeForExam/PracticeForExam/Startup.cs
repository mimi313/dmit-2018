using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracticeForExam.Startup))]
namespace PracticeForExam
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
