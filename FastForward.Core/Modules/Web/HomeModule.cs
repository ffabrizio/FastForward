using Nancy;

namespace FastForward.Core.Modules.Web
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["home", new { Title = "Home page" }];
        }
    }
}