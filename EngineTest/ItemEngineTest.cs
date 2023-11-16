using Engine.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.IO;
using Engine;
using Engine.Services.Abstractions;
using System.Linq;

namespace EngineTest
{
    public class ItemEngineTest
    {
        private IEngine Engine { get; set; }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            var execPath = Path.GetDirectoryName(GetType().Assembly.Location);
            var dbPath = Path.Combine(execPath, "test-db");

            if (Directory.Exists(dbPath)) Directory.Delete(dbPath, true);
            if (!Directory.Exists(dbPath)) Directory.CreateDirectory(dbPath);

            services.Configure<RepositoryOptions>(x => x.BasePath = dbPath);
            services.AddEngine();

            Engine = services.BuildServiceProvider().GetRequiredService<IEngine>();
        }

        [Test]
        public void TestCreateItem()
        {
            var item = Engine.Create("designs_test", new[] { new StringItemAttribute("attributes_example", "test") });
            Assert.IsNotEmpty(item.Id);
            Assert.AreEqual(item.DesignCode, "designs_test");
            
            var attribute = item.Attributes.Single();
            Assert.IsTrue(attribute is StringItemAttribute { Code: "attributes_example", Value: "test" });
        }
    }
}