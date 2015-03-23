using System;
using FastForward.Core.Business;

namespace FastForward.Core.ViewModels
{
    public class ApplicationVm : IVm
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime LastUpdate { get; set; } 
    }
}