using FastForward.Core.ViewModels;
using FastForward.Models;

namespace FastForward.Core.Business.Factories
{
    public class ApplicationVmFactory : IApplicationVmFactory
    {
        public ApplicationVm BuildVm(Application model)
        {
            return new ApplicationVm
            {
                Name = model.Name,
                Version = model.Version,
                LastUpdate = model.LastUpdate
            };
        }
    }
}