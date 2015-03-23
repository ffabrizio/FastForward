using System.Collections.Generic;

namespace ElasticSearchTests
{
    public class SimpleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Things { get; set; } 
    }
}