using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Nest;
using NUnit.Framework;

namespace ElasticSearchTests
{
    [TestFixture]
    public class SearchAndIndexing
    {
        const string server = "http://localhost:9200";
        const string defaultIndex = "test-application";
        const int numberOfTestItems = 100;

        ElasticClient client;

        [TestFixtureSetUp]
        public void Setup()
        {
            var node = new Uri(server);

            var settings = new ConnectionSettings(
                node,
                defaultIndex
            );

            client = new ElasticClient(settings);

            for (var counter = 0; counter < numberOfTestItems; counter++)
            {
                var app = new SimpleModel
                {
                    Id = counter.ToString(),
                    Name = counter == 0 ? "marker" : RandomData.RandomWord(),
                    Things = new List<string> { "home", "work" }
                };

                client.Index(app);

                Thread.Sleep(100);
            }

        }

        [Test]
        public void RetrieveDataByFullMatch()
        {
            var results = client.Search<SimpleModel>(s => s
                .From(0)
                .Query(q => q
                    .Term(a => a.Name, "marker")
                )
            );

            foreach (var model in results.Documents)
            {
                Console.WriteLine(model.Name);
            }

            Assert.AreEqual(1, results.Documents.Count());
        }

        [Test]
        public void RetrieveDataByNonMatchWithPaging()
        {
            var results = client.Search<SimpleModel>(s => s
                .From(0)
                .Size(5)
                .Query(q => !q
                    .Term(a => a.Name, "marker")
                )
            );

            foreach (var model in results.Documents)
            {
                Console.WriteLine(model.Name);
            }

            Assert.AreEqual(5, results.Documents.Count());
        }

        [Test]
        public void RetrieveDataByPartialMatchWithSortingAndPagingAndHighlights()
        {
            var results = client.Search<SimpleModel>(s => s
                .From(0)
                .Size(5)
                .SortDescending(q => q
                    .Id)
                .Query(q => q
                    .QueryString(a => a
                        .OnFields(p => p.Name, p => p.Things)
                        .Query("*ark*")
                    )
                )
                .Highlight(h => h
                    .PreTags("<b>")
                    .PostTags("</b>")
                    .OnFields(f => f
                        .OnField(e => e.Name)
                        .PreTags("<em>")
                        .PostTags("</em>")
                    )
                )
            );

            Assert.AreEqual(1, results.Documents.Count());

            foreach (var model in results.Documents)
            {
                Console.WriteLine(model.Name);
            }

            foreach (var key in results.Highlights.Keys)
            {
                Console.WriteLine(key);
                foreach (var value in results.Highlights[key])
                {
                    Console.WriteLine(value.Key + ":" + string.Join(", ", value.Value.Highlights));
                }
            }

            Assert.AreEqual(1, results.Highlights.Keys.Count());
            Assert.AreEqual(1, results.Highlights["0"].Keys.Count);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            client.DeleteIndex(defaultIndex);
        }
        
    }
}