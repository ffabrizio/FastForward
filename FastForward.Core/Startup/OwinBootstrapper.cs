using Owin;

namespace FastForward.Core.Startup
{
    public class OwinBootstrapper
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseNancy();
        }
    }
}