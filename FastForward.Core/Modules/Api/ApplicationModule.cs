using FastForward.Core.Business.Factories;
using FastForward.Data;
using Nancy;

namespace FastForward.Core.Modules.Api
{
    public class ApplicationModule : NancyModule
    {
        public ApplicationModule(IRepository repository, IApplicationVmFactory factory)
        {
            Get["/api/ping"] = _ => 
                Response.AsJson(factory.BuildVm(repository.GetApplication()));
        } 
    }
}