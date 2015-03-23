using System;

namespace FastForward.Models
{
    public class Application : IModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}