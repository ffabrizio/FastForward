using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace FastForward.Core.Modules.Api
{
    public class DynamicContentModule : NancyModule
    {
        public DynamicContentModule()
        {
            var factory = new ModelFactory();

            Get["/api/dynamic/model/{id}"] = _ => FormatterExtensions.AsJson(Response, factory.GetModel(_.id));

            Get["/api/dynamic/models/{type?}"] = _ =>
            {
                var page = 0;
                var size = 5;

                if (Request.Query.page.HasValue) int.TryParse(Request.Query.page, out page);
                if (Request.Query.size.HasValue) int.TryParse(Request.Query.size, out size);

                return FormatterExtensions.AsJson(Response, factory.GetModels(_.type, page, size));
            };
            
        }
    }

    public class ModelFactory
    {
        public dynamic GetModel(string id)
        {
            var model = data["Objects"].FirstOrDefault(v => v.Data.id == id) 
                ?? new Model("", new { });
            
            return model.Data;
        }

        public IEnumerable<dynamic> GetModels(string type = "", int page = 0, int size = 5)
        {
            var results = data["Objects"].Select(m => m.Data);
            if (!string.IsNullOrEmpty(type))
            {
                results = data["Objects"].Where(v => v.Collection == type).Select(m => m.Data);
            }

            return results.Skip(page * size).Take(size);
        }

        private static readonly Dictionary<string, List<Model>> data = new Dictionary<string, List<Model>>
        {
            { 
                "Objects", new List<Model>
                {
                    new Model("cats", new { id = "1", name = "Huxley", gender = "male", fur = "striped" }),
                    new Model("cats", new { id = "2", name = "Maisie", gender = "female", fur = "patchy" }),
                    new Model("dogs", new { id = "3", name = "Baroush", gender = "male", type = "German Shepherd" }),
                    new Model("dogs", new { id = "4", name = "Furia", gender = "female", type = "Mongrel" }),
                    new Model("dogs", new { id = "5", name = "Teal", gender = "male", type = "Golden Retriever" }),
                    new Model("dogs", new { id = "6", name = "Louis", gender = "male", type = "Labrador" }),
                    new Model("birds", new {  id = "7", name = "Yellow", gender = "male", type = "Parrot" }),
                    new Model("birds", new { id = "8", name = "Green", gender = "female", type = "Bird of Paradise" }),
                    new Model("snakes", new {  id = "9", name = "Yellow/Blue", gender = "female", type = "Viper" }),
                    new Model("snakes", new { id = "10", name = "Green", gender = "male", type = "RattleSnake" }),
                    new Model("bull", new { id = "11", name = "Fucking bull", gender = "male", type = "Horny" })
                    
                } 
            }
        };
    }

    public class Model
    {
        public Model(string collection, dynamic data)
        {
            Collection = collection;
            Data = data;
        }

        public string Collection { get; private set; }
        public dynamic Data { get; private set; }
    }
}