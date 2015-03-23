using FastForward.Core.Business.Factories;
using FastForward.Data;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;

namespace FastForward.Core.Startup
{
    public class IocBootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
        {
        }

        protected override void ConfigureApplicationContainer(IContainer existingContainer)
        {
            existingContainer.Configure(ioc =>
            {
                ioc.For<IRepository>().Use<Repository>();
                ioc.For<IApplicationVmFactory>().Use<ApplicationVmFactory>();
            });
        }

        protected override void ConfigureRequestContainer(IContainer container, NancyContext context)
        {
        }

        protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
        {
        }
    }
}